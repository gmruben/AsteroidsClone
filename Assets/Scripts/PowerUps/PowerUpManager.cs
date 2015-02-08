using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
	private float powerUpSpawnTime = 1;
	private float spawnCounter;

	public Transform[] spawnPointList;

	private PowerUp powerUp;
	private bool canSpawn;

	public void init()
	{
		/*this.gameCamera = gameCamera;
		this.levelConfig = levelConfig;*/

		spawnCounter = 0;
		canSpawn = true;
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

				powerUp = EntityManager.instantiatePowerUp("hola");
				powerUp.transform.position = position;

				powerUp.onPickUp += onPowerUpPickUp;
			}
		}
	}

	private void onPowerUpPickUp()
	{
		powerUp.onPickUp -= onPowerUpPickUp;
		canSpawn = true;
	}
}