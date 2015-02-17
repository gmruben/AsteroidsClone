using UnityEngine;
using System.Collections;

/// <summary>
/// Heavy Machine Gun controller shots a heavy bullet and shakes the screen
/// </summary>
public class HeavyMachineGunController : WeaponController
{	
	public HeavyMachineGunController(InputController inputController, Player player) : base(inputController, player)
	{
		coolDownTime = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.HeavyMachineGunCoolDownTime);
	}

	public override void shoot(Vector3 direction)
	{
		//Bullet bullet = EntityManager.instantiateHeavyBullet();

		float randomAngle = Random.Range(-5.0f, 5.0f);
		Vector2 randomDirection = Quaternion.AngleAxis(randomAngle, Vector3.back) * direction;

		//bullet.init(shooter as IShooter, shooter, bulletDirection);
		//bullet.transform.position = shooter.cachedTransform.position;

		shooter.shoot(randomDirection);

		GameCamera.instance.shake(0.25f, 0.75f);
	}
}