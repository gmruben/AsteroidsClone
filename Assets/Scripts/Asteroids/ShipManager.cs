using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class takes care of instantiating the ships. It is updated from the game, so it implements the IUpdateable interface.
/// </summary>
public class ShipManager : MonoBehaviour, IUpdateable
{
	//List with the different points an asteroid can be instantiated at
	public Transform[] spawnPointList;

	private float spawnCounter;
	private float asteroidCounter;
	private List<Ship> shipList;

	private float spawnTime;

	public void init()
	{
		spawnTime = GameParamConfig.instance.retrieveParamValue<int>(GameConfigParamIds.AsteroidSpawnTime);

		spawnCounter = 0;
		asteroidCounter = 0;

		shipList = new List<Ship>();
	}

	public void update(float time)
	{
		//Check if it is time to spawn a new asteroid
		spawnCounter += time;
		if (spawnCounter > spawnTime)
		{
			spawnCounter = 0;
			asteroidCounter++;

			//Get a random spawn position
			int randomIndex = UnityEngine.Random.Range(0, spawnPointList.Length);
			Vector3 position = spawnPointList[randomIndex].position;
			Vector3 direction = spawnPointList[randomIndex].up;

			instantiateAsteroid(PoolManager.PoolIds.AsteroidBig, position, direction);
		}
	}

	/// <summary>
	/// It destroys all the asteroids that are active at the moment
	/// </summary>
	public void clear()
	{
		spawnCounter = 0;
		asteroidCounter = 0;

		for (int i = 0; i < shipList.Count; i++)
		{
			PoolManager.instance.destroyInstance(shipList[i].GetComponent<PoolInstance>());
		}
	}

	public void instantiateAsteroid(PoolManager.PoolIds id, Vector3 position, Vector3 direction)
	{
		Ship ship = EntityManager.instantiateShip();

		ship.init(position, direction);
		shipList.Add(ship);
	}
}