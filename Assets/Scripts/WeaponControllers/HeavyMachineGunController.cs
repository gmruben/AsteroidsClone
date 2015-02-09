using UnityEngine;
using System.Collections;

public class HeavyMachineGunController : IWeaponController
{
	private InputController playerInput;
	private Player player;

	private bool isOn = false;
	private float coolDown = 0;
	
	public HeavyMachineGunController(InputController playerInput, Player player)
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
		if (coolDown > 0.10f)
		{
			coolDown = 0;
			shoot(player.cachedTransform.up);
		}
	}

	public void shoot(Vector3 direction)
	{
		Bullet bullet = EntityManager.instantiateHeavyBullet();

		float randomAngle = Random.Range(-5.0f, 5.0f);
		Vector2 bulletDirection = Quaternion.AngleAxis(randomAngle, Vector3.back) * direction;

		bullet.init(player, bulletDirection);
		bullet.transform.position = player.cachedTransform.position;

		GameCamera.instance.shake(0.25f, 0.75f);
	}
}