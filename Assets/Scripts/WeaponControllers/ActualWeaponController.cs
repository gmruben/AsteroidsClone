using UnityEngine;
using System.Collections;

/// <summary>
/// Weapon controller is an abstract class that controls the cooldown of weapons. It executes the shoot
/// function that is later implemented by each type of weapon.
/// </summary>
public abstract class ActualWeaponController
{
	protected InputController inputController;
	protected Player player;

	protected bool isActionUp = false;
	protected bool isActionDown = false;

	//The weapons have a cooldown time so they don't shoot at a high rate when pressing the action button down
	protected float coolDownTime;
	protected float coolDownCounter = 0;

	public ActualWeaponController(InputController inputController, Player player)
	{
		this.inputController = inputController;
		this.player = player;
	}

	public void startShoot()
	{
		isActionUp = false;
		isActionDown = true;
		
		if (coolDownCounter <= 0)
		{
			coolDownCounter = coolDownTime;
			shoot(player.cachedTransform.up);
		}
	}

	public void endShoot()
	{
		isActionUp = true;
	}

	public void update(float deltaTime)
	{
		if (isActionDown)
		{
			coolDownCounter -= deltaTime;
			if (coolDownCounter <= 0)
			{
				if (isActionUp)
				{
					isActionDown = false;
				}
				else if (isActionDown)
				{
					coolDownCounter = coolDownTime;
					shoot(player.cachedTransform.up);
				}
			}
		}
	}

	public abstract void shoot(Vector3 direction);
}