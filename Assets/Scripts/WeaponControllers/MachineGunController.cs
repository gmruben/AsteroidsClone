using UnityEngine;
using System.Collections;

/// <summary>
/// Machine Gun controller shots one simple bullet at a higher rate
/// </summary>
public class MachineGunController : WeaponController
{
	public MachineGunController(IShooter shooter) : base(shooter)
	{
		coolDownTime = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.MachineGunCoolDownTime);
	}

	public override void shoot(Vector3 direction)
	{
		Bullet bullet = EntityManager.instantiateBullet();
		shooter.shoot(bullet, direction);
	}
}