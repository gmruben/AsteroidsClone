using UnityEngine;
using System.Collections;

/// <summary>
/// Machine Gun controller shots one simple bullet at a higher rate
/// </summary>
public class MachineGunController : WeaponController
{
	public MachineGunController(InputController inputController, Player player) : base(inputController, player)
	{
		coolDownTime = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.MachineGunCoolDownTime);
	}

	public override void shoot(Vector3 direction)
	{
		//Bullet bullet = EntityManager.instantiateBullet();
		
		//bullet.init(shooter as IShooter, shooter, direction);
		//bullet.transform.position = shooter.cachedTransform.position;

		shooter.shoot(direction);
	}
}