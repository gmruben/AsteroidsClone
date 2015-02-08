using UnityEngine;
using System.Collections;

public class GunController : IWeaponController
{
	private PlayerInput playerInput;
	private Player player;

	private float coolDownTime;
	private float counter = 0;

	public GunController(PlayerInput playerInput, Player player)
	{
		this.playerInput = playerInput;
		this.player = player;

		coolDownTime = GameConfig.instance.retrieveParamValue<float>(GameConfigParamIds.GunCoolDownTime);
	}

	public void update(float deltaTime)
	{
		if (counter > 0)
		{
			counter -= deltaTime;
		}

		if (playerInput.isKeyDown(PlayerInputKeyIds.Action))
		{
			if (canShoot)
			{
				shoot(player.cachedTransform.up);
			}
		}
	}

	public void shoot(Vector3 direction)
	{
		counter = coolDownTime;

		Bullet bullet = EntityManager.instantiateBullet();

		bullet.init(player, direction);
		bullet.transform.position = player.cachedTransform.position;
	}

	private bool canShoot
	{
		get { return counter <= 0; }
	}
}