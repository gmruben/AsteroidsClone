using UnityEngine;
using System.Collections;

/// <summary>
/// Shotgun controller shots several bullets at the same time at a very low rate
/// </summary>
public class ShotGunController : WeaponController
{
	private const int numBullets = 5;

	public ShotGunController(InputController inputController, Player player) : base(inputController, player)
	{
		coolDownTime = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.ShotGunCoolDownTime);
	}

	public override void shoot(Vector3 direction)
	{
		for (int i = 0; i < numBullets; i++)
		{
			//Bullet bullet = EntityManager.instantiateBullet();

			float randomAngle = Random.Range(-10.0f, 10.0f);
			Vector2 randomDirection = Quaternion.AngleAxis(randomAngle, Vector3.back) * direction;

			//bullet.init(shooter as IShooter, shooter, randomDirection);
			//bullet.transform.position = shooter.cachedTransform.position;

			shooter.shoot(randomDirection);
		}
	}
}