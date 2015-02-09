using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Abstract class for all the power ups in the game
/// </summary>
public abstract class PowerUp : MonoBehaviour
{
	public event Action onPickUp;

	public GameObject sprite;

	protected Player player;
	
	protected bool isActive = false;
	protected float time;

	public abstract void pickUp(Player player);
	public abstract void end();

	void Awake()
	{
		MessageBus.onGamePause += onGamePause;
	}

	private void onGamePause(bool isPause)
	{
		isActive = !isPause;
	}

	protected void dispatchOnPickUp()
	{
		if (onPickUp != null) onPickUp();
	}
}

public class PowerUpIds
{
	public static string MachineGun = "MachineGun";
	public static string HeavyMachineGun = "HeavyMachineGun";

	//List with all the ids for the power ups (we can add or remove ids easily or create different lists)
	private static string[] powerUpList = new string[] { MachineGun, HeavyMachineGun };

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