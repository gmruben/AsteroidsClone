using UnityEngine;
using System.Collections;

public class MachineGunController : IWeaponController
{
	private InputController playerInput;
	private Player player;

	private bool isOn = false;
	private float coolDown = 0;
	
	public MachineGunController(InputController playerInput, Player player)
	{
		this.playerInput = playerInput;
		this.player = player;
	}
	
	public void update(float deltaTime)
	{
		if (playerInput.isKeyDown(PlayerInputKeyIds.Action))
		{
			isOn = true;
			shoot(player.cachedTransform.up);
		}
		else if (playerInput.isKeyUp(PlayerInputKeyIds.Action))
		{
			isOn = false;
			coolDown = 0;
		}

		if (isOn)
		{
			updateShoot(deltaTime);
		}
	}

	public void updateShoot(float deltaTime)
	{
		coolDown += deltaTime;
		if (coolDown > 0.15f)
		{
			coolDown = 0;
			shoot(player.cachedTransform.up);
		}
	}

	public void shoot(Vector3 direction)
	{
		Bullet bullet = EntityManager.instantiateBullet();
		
		bullet.init(player, direction);
		bullet.transform.position = player.cachedTransform.position;
	}
}