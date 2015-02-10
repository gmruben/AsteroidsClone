using UnityEngine;
using System.Collections;

public class PowerUp_HeavyMachineGun : PowerUp
{
	protected override void Update()
	{
		base.Update ();
	}

	public override void start()
	{	
		time = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.HeavyMachineGunPowerUpTime);

		player.setTimer(time);
		player.changeShootController(new HeavyMachineGunController(player.inputController, player));
	}

	public override void end()
	{
		player.endTimer();
		player.changeShootController(new GunController(player.inputController, player));

		GameObject.Destroy(gameObject);
	}
}