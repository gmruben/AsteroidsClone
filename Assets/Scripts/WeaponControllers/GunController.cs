using UnityEngine;
using System.Collections;

/// <summary>
/// Gun controller just shots one simple bullet
/// </summary>
public class GunController : WeaponController
{
	public GunController(IShooter shooter) : base(shooter)
	{
		coolDownTime = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.GunCoolDownTime);
	}
	
	public override void shoot(Vector3 direction)
	{
		Bullet bullet = EntityManager.instantiateBullet();
		shooter.shoot(bullet, direction);
	}
}