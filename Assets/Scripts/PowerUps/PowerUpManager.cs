using UnityEngine;
using System.Collections;

/// <summary>
/// This class takes care of instantiating the power ups. It is updated from the game, so it implements the IUpdateable interface.
/// </summary>
public class PowerUpManager : MonoBehaviour, IUpdateable
{
	private float powerUpSpawnTime;
	private float spawnCounter;

	//List with the different points a power up can be instantiated at
	public Transform[] spawnPointList;

	private PowerUp powerUp;
	private bool canSpawn;

	public void init()
	{
		spawnCounter = 0;
		canSpawn = true;

		powerUpSpawnTime = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.PowerUpSpawnTime);
	}

	public void update(float time)
	{
		if (canSpawn)
		{
			spawnCounter += time;
			if (spawnCounter > powerUpSpawnTime)
			{
				spawnCounter = 0;
				canSpawn = false;

				int randomIndex = UnityEngine.Random.Range(0, spawnPointList.Length);
				Vector3 position = spawnPointList[randomIndex].position;

				powerUp = EntityManager.instantiatePowerUp(PowerUpIds.retrieveRandomPowerUpId());
				powerUp.transform.position = position;

				powerUp.onPickUp += onPowerUpPickUp;
			}
		}
	}

	public void clear()
	{
		spawnCounter = 0;
		canSpawn = true;
		
		if (powerUp != null) GameObject.Destroy(powerUp.gameObject);
	}

	private void onPowerUpPickUp()
	{
		powerUp.onPickUp -= onPowerUpPickUp;
		canSpawn = true;
	}
}