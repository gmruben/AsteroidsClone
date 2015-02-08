﻿using UnityEngine;
using System.Collections;

public class Asteroid_Medium : Asteroid
{
	public override void initParams()
	{
		score = GameConfig.instance.retrieveParamValue<int>(GameConfigParamIds.AsteroidMediumScore);
		speed = GameConfig.instance.retrieveParamValue<float>(GameConfigParamIds.AsteroidMediumSpeed);
	}

	public override void hit(Vector3 position, Vector3 direction)
	{
		Vector3 direction1 = Quaternion.AngleAxis(90, Vector3.back) * direction;
		Vector3 direction2 = Quaternion.AngleAxis(-90, Vector3.back) * direction;

		asteroidManager.instantiateAsteroid("asteroid_small", position, direction1);
		asteroidManager.instantiateAsteroid("asteroid_small", position, direction2);

		PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
	}

	public override void kill()
	{
		PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());

		CustomParticleEmitter customParticleEmitter = new CustomParticleEmitter();
		customParticleEmitter.explode2(cachedTransform.position);
	}
}