using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour
{
	public event Action onEnd;

	private const float spawnTime = 2.5f;

	public Transform[] spawnPointList;

	private float spawnCounter;
	private float asteroidCounter;
	private List<Asteroid> asteroidList;

	private GameCamera gameCamera;
	private LevelConfig levelConfig;

	public void init(GameCamera gameCamera, LevelConfig levelConfig)
	{
		this.gameCamera = gameCamera;
		this.levelConfig = levelConfig;

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
			instantiateAsteroid();
		}
	}

	private void instantiateAsteroid()
	{
		Asteroid asteroid = EntityManager.instantiateAsteroid();

		int randomIndex = UnityEngine.Random.Range(0, spawnPointList.Length);

		asteroid.init(gameCamera, spawnPointList[randomIndex].position, spawnPointList[randomIndex].up);
		asteroidList.Add(asteroid);
	}
}

public class LevelConfig
{
	public float spawnTime;
	public int numAsteroids;
}