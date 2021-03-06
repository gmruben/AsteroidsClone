using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	private const float life = 2.0f;
	private const float speed = 25.0f;

	private Transform cachedTransform;
	private SpriteRenderer spriteRenderer;

	public IShooter shooter { get; private set; }

	private Player player;
	private Vector3 direction;

	private float lifeTimer;
	private bool isActive = true;

	private Warper warper;

	void Awake()
	{
		cachedTransform = transform;
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		//Create Warper
		warper = new Warper (cachedTransform);

		//Susbribe to game pause and game end events
		MessageBus.onGamePause += onGamePause;
		MessageBus.onGameEnd += onGameEnd;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		IHittable hittable = other.GetComponent(typeof(IHittable)) as IHittable;
		if (hittable != null)
		{
			hittable.hit(this, cachedTransform.position, direction);
		}
	}

	public void init(Color color, IShooter shooter, Vector3 direction)
	{
		this.shooter = shooter;
		this.direction = direction;

		lifeTimer = life;
		spriteRenderer.color = color;
	}

	void Update()
	{
		if (isActive)
		{
			cachedTransform.position += direction * speed * Time.deltaTime;
			warper.checkWarp();

			//When a bullet dies it is sent back to the pool
			lifeTimer -= Time.deltaTime;
			if (lifeTimer < 0) PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
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