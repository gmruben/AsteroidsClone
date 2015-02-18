using UnityEngine;
using System.Collections;

public class PowerUp_MachineGun : PowerUp
{
	protected override void Update()
	{
		base.Update ();
	}

	public override void start()
	{
		time = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.MachineGunPowerUpTime);

		player.playerTimer.startTimer(time);
		player.changeShootController(new MachineGunController(player));
	}

	public override void end()
	{
		player.playerTimer.endTimer();
		player.changeShootController(new GunController(player));

		GameObject.Destroy(gameObject);
	}
}