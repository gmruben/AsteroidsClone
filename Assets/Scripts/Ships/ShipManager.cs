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

	//List with the transform for all the possible targets
	private List<Transform> targetList;

	private float spawnCounter;
	private List<Ship> shipList;

	private float spawnTime;

	public void init(List<Transform> targetList)
	{
		spawnTime = GameParamConfig.instance.retrieveParamValue<int>(GameConfigParamIds.ShipSpawnTime);

		this.targetList = targetList;

		spawnCounter = 0;
		shipList = new List<Ship>();
	}

	public void update(float time)
	{
		//Check if it is time to spawn a new asteroid
		spawnCounter += time;
		if (spawnCounter > spawnTime)
		{
			spawnCounter = 0;

			//Get a random spawn position
			int randomIndex = UnityEngine.Random.Range(0, spawnPointList.Length);
			Vector3 position = spawnPointList[randomIndex].position;
			Vector3 direction = spawnPointList[randomIndex].up;

			instantiateShip(PoolManager.PoolIds.AsteroidBig, position, direction);
		}
	}

	/// <summary>
	/// It destroys all the ships that are active at the moment
	/// </summary>
	public void dispose()
	{
		spawnCounter = 0;
		for (int i = 0; i < shipList.Count; i++)
		{
			PoolManager.instance.destroyInstance(shipList[i].GetComponent<PoolInstance>());
		}
	}

	public void instantiateShip(PoolManager.PoolIds id, Vector3 position, Vector3 direction)
	{
		Ship ship = EntityManager.instantiateShip();

		//Select a random target
		int randomIndex = UnityEngine.Random.Range(0, targetList.Count);
		Transform target = targetList[randomIndex];

		ship.init(target, position, direction);
		shipList.Add(ship);
	}
}