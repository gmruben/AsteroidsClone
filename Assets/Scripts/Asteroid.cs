using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour, IAsteroid
{
	public int score = 5;
	private const float speed = 5.0f;

	private Transform cachedTransform;
	private Vector3 direction;

	private GameCamera gameCamera;

	private WarpController warpController;

	void Awake()
	{
		cachedTransform = transform;
	}

	void Update()
	{
		cachedTransform.position += direction * speed * Time.deltaTime;
		warpController.checkWarp();
	}

	public void init(GameCamera gameCamera, Vector3 position, Vector3 direction)
	{
		this.gameCamera = gameCamera;
		this.direction = direction;

		cachedTransform.position = position;
		warpController = new WarpController(gameCamera, cachedTransform);
	}

	public void hit(Vector3 position, Vector3 direction)
	{
		Vector3 direction1 = Quaternion.AngleAxis(30, Vector3.back) * direction;
		Vector3 direction2 = Quaternion.AngleAxis(-30, Vector3.back) * direction;

		Asteroid asteroid1 = EntityManager.instantiateAsteroid();
		asteroid1.init(gameCamera, position, direction1);

		Asteroid asteroid2 = EntityManager.instantiateAsteroid();
		asteroid2.init(gameCamera, position, direction2);

		PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
	}
}