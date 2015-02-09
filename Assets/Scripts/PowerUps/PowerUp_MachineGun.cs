using UnityEngine;
using System.Collections;

public class PowerUp_MachineGun : PowerUp
{
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

	void Update()
	{
		if (isActive)
		{
			time -= Time.deltaTime;
			player.updateTimer(time);
			if (time <= 0)
			{
				end ();
			}
		}
	}

	public override void end()
	{
		player.endTimer();
		player.changeShootController(new GunController(player.inputController, player));

		GameObject.Destroy(gameObject);
	}
}