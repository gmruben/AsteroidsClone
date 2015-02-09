using UnityEngine;
using System.Collections;

public class PowerUp_HeavyMachineGun : PowerUp
{
	void Update()
	{
		if (isActive)
		{
			time -= Time.deltaTime;
			if (time <= 0)
			{
				end ();
			}
		}
	}

	public override void pickUp(Player player)
	{
		this.player = player;

		isActive = true;
		time = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.HeavyMachineGunPowerUpTime);

		player.setTimer(time);
		player.changeShootController(new HeavyMachineGunController(player.inputController, player));

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