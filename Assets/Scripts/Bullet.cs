using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	private const float life = 2.0f;
	private const float speed = 25.0f;

	private Transform cachedTransform;

	private GameCamera gameCamera;
	private Player player;
	private Vector3 direction;

	private float lifeTimer;

	public void init(GameCamera gameCamera, Player player, Vector3 direction)
	{
		cachedTransform = transform;

		this.gameCamera = gameCamera;
		this.player = player;
		this.direction = direction;

		lifeTimer = life;
	}

	void Update()
	{
		cachedTransform.position += direction * speed * Time.deltaTime;

		Vector3 point = gameCamera.camera.WorldToViewportPoint(cachedTransform.position);
		
		if (point.x < 0.0f || point.x > 1.0f) cachedTransform.position = new Vector3(-cachedTransform.position.x, cachedTransform.position.y, cachedTransform.position.z);
		if (point.y < 0.0f || point.y > 1.0f) cachedTransform.position = new Vector3(cachedTransform.position.x, -cachedTransform.position.y, cachedTransform.position.z);

		lifeTimer -= Time.deltaTime;
		if (lifeTimer < 0) PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(TagNames.Asteroid))
		{
			Asteroid asteroid = other.GetComponent<Asteroid>();
			asteroid.hit(cachedTransform.position, direction);

			CustomParticleEmitter customParticleEmitter = new CustomParticleEmitter();

			customParticleEmitter.init();
			customParticleEmitter.explode(cachedTransform.position, -direction);

			player.addScore(asteroid.score);
			PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());

			gameCamera.shake(0.25f, 0.25f);
		}
	}
}