using UnityEngine;
using System.Collections;

public class Asteroid_Small : Asteroid
{
	public override void initParams()
	{
		//Get the config parameters for the small asteroids
		score = GameParamConfig.instance.retrieveParamValue<int>(GameConfigParamIds.AsteroidSmallScore);
		speed = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.AsteroidSmallSpeed);
	}

	public override void hit(Vector3 position, Vector3 direction)
	{
		//When a medium asteroid is hit, they dont create any more asteroids
		PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
		customParticleEmitter.explosion(Color.white, cachedTransform.position);
	}
}