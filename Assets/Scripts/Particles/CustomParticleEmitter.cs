﻿using UnityEngine;
using System.Collections;

/// <summary>
/// A very simple particle emitter that only controls speed and angle of emission
/// </summary>
public class CustomParticleEmitter
{
	private const int numParticles = 25;

	public void hit(Color color, Vector3 position, Vector2 direction)
	{
		for (int i = 0; i < numParticles; i++)
		{
			float randomSpeed = Random.Range(5.0f, 25.0f);
			float randomAngle = Random.Range(-25.0f, 25.0f);
			Vector2 particleDirection = Quaternion.AngleAxis(randomAngle, Vector3.back) * direction;

			instantiateParticle(position, particleDirection, randomSpeed, color);
		}
	}

	public void explosion(Color color, Vector3 position)
	{
		for (int i = 0; i < numParticles; i++)
		{
			float randomSpeed = Random.Range(5.0f, 25.0f);
			float randomAngle = Random.Range(-0.0f, 360.0f);
			Vector2 particleDirection = Quaternion.AngleAxis(randomAngle, Vector3.back) * Vector3.up;

			instantiateParticle(position, particleDirection, randomSpeed, color);
		}
	}

	private void instantiateParticle(Vector3 position, Vector3 direction, float speed, Color color)
	{
		CustomParticle particle = EntityManager.instantiateParticle();

		particle.transform.position = position;
		particle.init(speed, direction, color);
	}
}