﻿using UnityEngine;
using System.Collections;

public abstract class Asteroid : MonoBehaviour
{
	public int score { get; protected set; }
	public float speed { get; protected set; }

	protected Transform cachedTransform;
	private Vector3 direction;

	protected AsteroidManager asteroidManager;

	private WarpController warpController;

	private bool isActive = true;

	void Awake()
	{
		cachedTransform = transform;
		MessageBus.onGamePause += onGamePause;
	}

	void Update()
	{
		if (isActive)
		{
			cachedTransform.position += direction * speed * Time.deltaTime;
			warpController.checkWarp();
		}
	}

	public void init(AsteroidManager asteroidManager, Vector3 position, Vector3 direction)
	{
		this.asteroidManager = asteroidManager;
		this.direction = direction;

		cachedTransform.position = position;
		warpController = new WarpController(cachedTransform);

		initParams();
	}

	public abstract void initParams();
	public abstract void hit(Vector3 position, Vector3 direction);
	public abstract void kill();

	private void onGamePause(bool isPause)
	{
		isActive = !isPause;
	}
}