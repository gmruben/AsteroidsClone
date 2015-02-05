using UnityEngine;
using System.Collections;

public class CustomParticleEmitter
{
	private const int numParticles = 25;

	private CustomParticle[] particleList;

	public void init()
	{
		particleList = new CustomParticle[numParticles];
		for (int i = 0; i < numParticles; i++)
		{
			particleList[i] = EntityManager.instantiateParticle();
		}
	}

	public void explode(Vector3 position, Vector2 direction)
	{
		for (int i = 0; i < numParticles; i++)
		{
			float randomSpeed = Random.Range(5.0f, 25.0f);
			float randomAngle = Random.Range(-25.0f, 25.0f);

			Vector2 particleDirection = Quaternion.AngleAxis(randomAngle, Vector3.back) * direction;

			particleList[i].transform.position = position;
			particleList[i].init(randomSpeed, particleDirection);
		}
	}
}