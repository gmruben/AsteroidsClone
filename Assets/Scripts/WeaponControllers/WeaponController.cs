using UnityEngine;
using System.Collections;

/// <summary>
/// Weapon controller is an abstract class that controls the cooldown of weapons. It executes the shoot
/// function that is later implemented by each type of weapon.
/// </summary>
public abstract class WeaponController
{
	protected IShooter shooter;

	protected bool isActionUp = false;
	protected bool isActionDown = false;

	//The weapons have a cooldown time so they don't shoot at a high rate when pressing the action button down
	protected float coolDownTime;
	protected float coolDownCounter = 0;

	public WeaponController(IShooter shooter)
	{
		this.shooter = shooter;
	}

	public void startShoot()
	{
		isActionUp = false;
		isActionDown = true;
		
		if (coolDownCounter <= 0)
		{
			coolDownCounter = coolDownTime;
			shoot(shooter.shootDirection);
		}
	}

	public void endShoot()
	{
		isActionUp = true;
	}

	public void oneShot()
	{
		startShoot ();
		endShoot ();
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
					shoot(shooter.shootDirection);
				}
			}
		}
	}

	public abstract void shoot(Vector3 direction);
}