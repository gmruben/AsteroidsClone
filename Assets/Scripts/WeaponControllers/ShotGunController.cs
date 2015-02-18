using UnityEngine;
using System.Collections;

/// <summary>
/// Shotgun controller shots several bullets at the same time at a very low rate
/// </summary>
public class ShotGunController : WeaponController
{
	private const int numBullets = 5;

	public ShotGunController(IShooter shooter) : base(shooter)
	{
		coolDownTime = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.ShotGunCoolDownTime);
	}

	public override void shoot(Vector3 direction)
	{
		for (int i = 0; i < numBullets; i++)
		{
			float randomAngle = Random.Range(-10.0f, 10.0f);
			Vector2 randomDirection = Quaternion.AngleAxis(randomAngle, Vector3.back) * direction;

			Bullet bullet = EntityManager.instantiateBullet();
			shooter.shoot(bullet, randomDirection);
		}
	}
}