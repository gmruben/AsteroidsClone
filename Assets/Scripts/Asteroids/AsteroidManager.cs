using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class takes care of instantiating the asteroids. It is updated from the game, so it implements the IUpdateable interface.
/// </summary>
public class AsteroidManager : MonoBehaviour, IUpdateable
{
	//List with the different points an asteroid can be instantiated at
	public Transform[] spawnPointList;

	private float spawnCounter;
	private float asteroidCounter;
	private List<Asteroid> asteroidList;

	private float spawnTime = 5.0f;

	public void init()
	{
		spawnCounter = 0;
		asteroidCounter = 0;

		asteroidList = new List<Asteroid>();
	}

	public void update(float time)
	{
		spawnCounter += time;
		if (spawnCounter > spawnTime)
		{
			spawnCounter = 0;
			asteroidCounter++;

			int randomIndex = UnityEngine.Random.Range(0, spawnPointList.Length);
			Vector3 position = spawnPointList[randomIndex].position;
			Vector3 direction = spawnPointList[randomIndex].up;

			instantiateAsteroid(PoolIds.AsteroidBig, position, direction);
		}
	}

	public void clear()
	{
		spawnCounter = 0;
		asteroidCounter = 0;

		for (int i = 0; i < asteroidList.Count; i++)
		{
			PoolManager.instance.destroyInstance(asteroidList[i].GetComponent<PoolInstance>());
		}
	}

	public void instantiateAsteroid(string id, Vector3 position, Vector3 direction)
	{
		Asteroid asteroid = EntityManager.instantiateAsteroid(id);

		asteroid.init(this, position, direction);
		asteroidList.Add(asteroid);
	}
}