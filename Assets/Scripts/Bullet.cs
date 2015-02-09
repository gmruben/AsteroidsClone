using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	private const float life = 2.0f;
	private const float speed = 25.0f;

	private Transform cachedTransform;

	private Player player;
	private Vector3 direction;

	private float lifeTimer;
	private bool isActive = true;

	private Warper warper;
	private CustomParticleEmitter customParticleEmitter;

	void Awake()
	{
		cachedTransform = transform;
		customParticleEmitter = new CustomParticleEmitter();

		//Create Warper
		warper = new Warper (cachedTransform);

		//Susbribe to game pause and game end events
		MessageBus.onGamePause += onGamePause;
		MessageBus.onGameEnd += onGameEnd;
	}

	public void init(Player player, Vector3 direction)
	{
		this.player = player;
		this.direction = direction;

		lifeTimer = life;
	}

	void Update()
	{
		if (isActive)
		{
			cachedTransform.position += direction * speed * Time.deltaTime;
			warper.checkWarp();

			lifeTimer -= Time.deltaTime;
			if (lifeTimer < 0) PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(TagNames.Asteroid))
		{
			Asteroid asteroid = other.GetComponent<Asteroid>();
			asteroid.hit(cachedTransform.position, direction);

			customParticleEmitter.hit(Color.white, cachedTransform.position, -direction);

			player.addScore(asteroid.score);
			PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());

			GameCamera.instance.shake(0.25f, 0.25f);
		}
	}

	/// <summary>
	/// Called when the game is paused/unpaused
	/// </summary>
	/// <param name="isPause">If <c>true</c> is pause.</param>
	private void onGamePause(bool isPause)
	{
		isActive = !isPause;
	}

	/// <summary>
	/// Called when the game is ended
	/// </summary>
	private void onGameEnd()
	{
		PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
	}
}