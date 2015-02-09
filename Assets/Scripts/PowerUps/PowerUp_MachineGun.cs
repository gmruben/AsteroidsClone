using UnityEngine;
using System.Collections;

public class PowerUp_MachineGun : PowerUp
{
	protected override void Update()
	{
		base.Update ();
	}

	public override void pickUp(Player player)
	{
		this.player = player;

		isActive = true;
		time = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.MachineGunPowerUpTime);

		player.setTimer(time);
		player.changeShootController(new MachineGunController(player.inputController, player));

		sprite.gameObject.SetActive(false);
		dispatchOnPickUp();
	}

	public override void end()
	{
		player.endTimer();
		player.changeShootController(new GunController(player.inputController, player));

		GameObject.Destroy(gameObject);
	}
}