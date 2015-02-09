using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour, IUpdateable
{
	private float powerUpSpawnTime;
	private float spawnCounter;

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

		//if (powerUp != null) GameObject.Destroy(powerUp.gameObject);
	}
}