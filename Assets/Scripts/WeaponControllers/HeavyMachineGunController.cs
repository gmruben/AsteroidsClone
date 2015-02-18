using UnityEngine;
using System.Collections;

/// <summary>
/// Heavy Machine Gun controller shots a heavy bullet and shakes the screen
/// </summary>
public class HeavyMachineGunController : WeaponController
{	
	public HeavyMachineGunController(IShooter shooter) : base(shooter)
	{
		coolDownTime = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.HeavyMachineGunCoolDownTime);
	}

	public override void shoot(Vector3 direction)
	{
		float randomAngle = Random.Range(-5.0f, 5.0f);
		Vector2 randomDirection = Quaternion.AngleAxis(randomAngle, Vector3.back) * direction;

		Bullet bullet = EntityManager.instantiateHeavyBullet();
		shooter.shoot(bullet, randomDirection);

		GameCamera.instance.shake(0.25f, 0.75f);
	}
}