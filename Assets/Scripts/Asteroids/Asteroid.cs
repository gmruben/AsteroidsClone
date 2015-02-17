using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class for all the different asteroids. The child classes have to implement the initParam
/// and the hit functions.
/// </summary>
public abstract class Asteroid : MonoBehaviour, IHittable
{
	public int score { get; protected set; }
	public float speed { get; protected set; }

	protected Transform cachedTransform;
	private Vector3 direction;

	protected AsteroidManager asteroidManager;

	private Warper warper;
	protected CustomParticleEmitter customParticleEmitter;

	private bool isActive = true;

	void Awake()
	{
		cachedTransform = transform;
		MessageBus.onGamePause += onGamePause;
	}

	void Update()
	{
		if (isActive)
		{
			cachedTransform.position += direction * speed * Time.deltaTime;
			warper.checkWarp();
		}
	}
	
	public void init(AsteroidManager asteroidManager, Vector3 position, Vector3 direction)
	{
		this.asteroidManager = asteroidManager;
		this.direction = direction;

		cachedTransform.position = position;

		customParticleEmitter = new CustomParticleEmitter();
		warper = new Warper(cachedTransform);

		initParams();
	}

	//Initialises the parameters for the asteroid
	public abstract void initParams();
	public abstract void hit(Bullet bullet, IShooter shooter, Vector3 position, Vector3 direction);

	//Called when the game is paused
	private void onGamePause(bool isPause)
	{
		isActive = !isPause;
	}
}