using UnityEngine;
using System.Collections;

/// <summary>
/// Gun controller just shots one simple bullet
/// </summary>
public class GunController : WeaponController
{
	public GunController(InputController inputController, Player player) : base(inputController, player)
	{
		coolDownTime = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.GunCoolDownTime);
	}
	
	public override void shoot(Vector3 direction)
	{
		/*Bullet bullet = EntityManager.instantiateBullet();

		bullet.init(shooter as IShooter, shooter, direction);
		bullet.transform.position = shooter.position;*/

		shooter.shoot(direction);
	}
}