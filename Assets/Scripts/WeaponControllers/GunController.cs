using UnityEngine;
using System.Collections;

public class GunController : IWeaponController
{
	private GameCamera gameCamera;
	private PlayerInput playerInput;
	private Player player;

	public GunController(GameCamera gameCamera, PlayerInput playerInput, Player player)
	{
		this.gameCamera = gameCamera;
		this.playerInput = playerInput;
		this.player = player;
	}

	public void update(float deltaTime)
	{
		if (playerInput.isKeyDown(PlayerInputKeyIds.Action))
		{
			shoot(player.cachedTransform.up);
		}
	}

	public void shoot(Vector3 direction)
	{
		Bullet bullet = EntityManager.instantiateBullet();

		bullet.init(gameCamera, player, direction);
		bullet.transform.position = player.cachedTransform.position;
	}
}