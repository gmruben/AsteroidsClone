using UnityEngine;
using System.Collections;

public class PowerUp_ShotGun : PowerUp
{
	protected override void Update()
	{
		base.Update ();
	}

	public override void start()
	{
		time = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.ShotGunPowerUpTime);

		player.playerTimer.startTimer(time);
		player.changeShootController(new ShotGunController(player));
	}

	public override void end()
	{
		player.playerTimer.endTimer();
		player.changeShootController(new GunController(player));

		GameObject.Destroy(gameObject);
	}
}