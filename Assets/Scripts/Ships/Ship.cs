using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour, IHittable, IShooter, IEnemy
{
	public int score { get; protected set; }
	public float speed { get; protected set; }

	protected Transform cachedTransform;

	private Transform target;
	private Vector3 direction;

	private Warper warper;
	private bool isActive = true;

	private CustomParticleEmitter customParticleEmitter;

	private float shootTime = 0.0f;
	private float shootFrequency = 5.0f;

	private WeaponController weaponController;

	void Awake()
	{
		cachedTransform = transform;

		//Get the config parameters for the ship
		score = GameParamConfig.instance.retrieveParamValue<int>(GameConfigParamIds.ShipScore);
		speed = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.ShipSpeed);

		MessageBus.onGamePause += onGamePause;
	}

	void Update()
	{
		if (isActive)
		{
			cachedTransform.position += direction * speed * Time.deltaTime;
			warper.checkWarp();

			weaponController.update(Time.deltaTime);
			updateShoot();
		}
	}

	private void updateShoot()
	{
		//Update shot
		shootTime += Time.deltaTime;
		if (shootTime > shootFrequency)
		{
			weaponController.oneShot();
			shootTime = 0.0f;
		}
	}

	public void init(Transform target, Vector3 position, Vector3 direction)
	{
		this.target = target;
		this.direction = direction;
		
		cachedTransform.position = position;
		
		customParticleEmitter = new CustomParticleEmitter();
		warper = new Warper(cachedTransform);

		//Create the weapon controller
		weaponController = new GunController(this);
	}

	public void hit(Bullet bullet, Vector3 position, Vector3 direction)
	{
		if (bullet.shooter is Player)
		{
			Player other = bullet.shooter as Player;
			other.addScore(score);
				
			customParticleEmitter.hit(other.color, position, -direction);

			//Destroy the ship and the bullet
			PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
			PoolManager.instance.destroyInstance(bullet.GetComponent<PoolInstance>());

			GameCamera.instance.shake(0.25f, 0.25f);
		}
	}

	public void shoot(Bullet bullet, Vector3 direction)
	{
		bullet.init(Color.white, this, direction);
		bullet.transform.position = cachedTransform.position;
	}

	private void onGamePause(bool isPause)
	{
		isActive = !isPause;
	}

	public Vector2 shootDirection
	{
		get
		{
			Vector3 direction = target.position - cachedTransform.position;
			return ((Vector2) direction).normalized;
		}
	}
}