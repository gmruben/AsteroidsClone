using UnityEngine;
using System.Collections;

public class PowerUp_HeavyMachineGun : PowerUp
{
	private Player player;

	private bool isActive = false;
	private float time;

	public GameCamera gameCamera;

	public override void pickUp(Player player)
	{
		this.player = player;

		isActive = true;
		time = 2.5f;

		player.setTimer(2.5f);
		player.changeShootController(new HeavyMachineGunController(gameCamera, player.playerInput, player));

		sprite.gameObject.SetActive(false);
	}

	void Update()
	{
		if (isActive)
		{
			time -= Time.deltaTime;
			if (time <= 0) end ();
		}
	}

	public void end()
	{
		player.changeShootController(new GunController(gameCamera, player.playerInput, player));
		GameObject.Destroy(gameObject);
	}
}