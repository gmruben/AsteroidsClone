using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Abstract class for all the power ups in the game. It executes the start and end
/// functions that are later implemented by each type of weapon.
/// </summary>
public abstract class PowerUp : MonoBehaviour
{
	public event Action onPickUp;

	public GameObject sprite;

	protected Player player;
	protected Collider2D cachedCollider;
	
	protected bool isActive = false;
	protected float time;

	void Awake()
	{
		cachedCollider = GetComponent<Collider2D>();
		MessageBus.onGamePause += onGamePause;
	}

	protected virtual void Update()
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

	public void pickUp(Player player)
	{
		this.player = player;
		isActive = true;
		
		start ();
		
		sprite.gameObject.SetActive(false);
		dispatchOnPickUp();
	}

	//Start the power up effect
	public abstract void start();
	//Ends the power up effect
	public abstract void end();

	private void onGamePause(bool isPause)
	{
		isActive = !isPause;
	}

	protected void dispatchOnPickUp()
	{
		//Disable collider son no one else can pick it up
		cachedCollider.enabled = false;
		if (onPickUp != null) onPickUp();
	}
}

public class PowerUpIds
{
	public static string MachineGun = "MachineGun";
	public static string HeavyMachineGun = "HeavyMachineGun";
	public static string ShotGun = "ShotGun";

	//List with all the ids for the power ups (we can add or remove ids easily or create different lists)
	private static string[] powerUpList = new string[] { MachineGun, HeavyMachineGun, ShotGun };

	/// <summary>
	/// Retrieves a random id from the power up id list
	/// </summary>
	/// <returns>The random id.</returns>
	public static string retrieveRandomPowerUpId()
	{
		int random = UnityEngine.Random.Range(0, powerUpList.Length);
		return powerUpList[random];
	}
}