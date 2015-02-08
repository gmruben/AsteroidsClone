using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour, IUpdatable
{
	public event Action onEnd;

	public Transform[] spawnPointList;

	private float spawnCounter;
	private float asteroidCounter;
	private List<Asteroid> asteroidList;
	
	private LevelConfig levelConfig;

	public void init(LevelConfig levelConfig)
	{
		this.levelConfig = levelConfig;

		spawnCounter = 0;
		asteroidCounter = 0;

		asteroidList = new List<Asteroid>();
	}

	public void update(float time)
	{
		spawnCounter += time;
		if (spawnCounter > levelConfig.spawnTime)
		{
			spawnCounter = 0;
			asteroidCounter++;

			int randomIndex = UnityEngine.Random.Range(0, spawnPointList.Length);
			Vector3 position = spawnPointList[randomIndex].position;
			Vector3 direction = spawnPointList[randomIndex].up;

			instantiateAsteroid("asteroid", position, direction);
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

public class LevelConfig
{
	public float spawnTime;
	public int numAsteroids;
}