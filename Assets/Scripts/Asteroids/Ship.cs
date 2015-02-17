using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour, IHittable, IShooter
{
	public int score { get; protected set; }
	public float speed { get; protected set; }

	protected Transform cachedTransform;
	private Vector3 direction;

	private Warper warper;
	private bool isActive = true;

	private CustomParticleEmitter customParticleEmitter;

	private float shootTime = 0.0f;
	private float shootFrequency = 5.0f;

	private ActualWeaponController actualWeaponController;

	void Update()
	{
		if (isActive)
		{
			cachedTransform.position += direction * speed * Time.deltaTime;
			warper.checkWarp();

			updateShoot();
		}
	}

	private void updateShoot()
	{
		//Update shot
		shootTime += Time.deltaTime;
		if (shootTime > shootFrequency)
		{
			//SHOOT!
			actualWeaponController.startShoot();
			actualWeaponController.endShoot();

			shootTime = 0.0f;
		}
	}

	public void init(Vector3 position, Vector3 direction)
	{
		this.direction = direction;
		
		cachedTransform.position = position;
		
		customParticleEmitter = new CustomParticleEmitter();
		warper = new Warper(cachedTransform);
		
		//initParams();

		//Create the weapong controller
		//actualWeaponController = new GunController();
	}

	public void hit(Bullet bullet, IShooter shooter, Vector3 position, Vector3 direction)
	{
		if (shooter is Player)
		{
			Player other = shooter as Player;
			other.hit();
				
			customParticleEmitter.hit(Color.white, position, -direction);
			
			PoolManager.instance.destroyInstance(bullet.GetComponent<PoolInstance>());
			GameCamera.instance.shake(0.25f, 0.25f);
		}
	}

	public void shoot(Vector3 direction)
	{
		Bullet bullet = EntityManager.instantiateBullet();
		
		bullet.init(Color.white, this, direction);
		bullet.transform.position = cachedTransform.position;
	}

	public Vector2 shootDirection
	{
		//Make this random
		get { return Vector2.up; }
	}
}