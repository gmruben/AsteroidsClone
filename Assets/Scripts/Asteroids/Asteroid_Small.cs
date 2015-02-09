using UnityEngine;
using System.Collections;

public class Asteroid_Small : Asteroid
{
	public override void initParams()
	{
		score = GameParamConfig.instance.retrieveParamValue<int>(GameConfigParamIds.AsteroidSmallScore);
		speed = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.AsteroidSmallSpeed);
	}

	public override void hit(Vector3 position, Vector3 direction)
	{
		kill ();
	}

	public override void kill()
	{
		PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
		customParticleEmitter.explosion(Color.white, cachedTransform.position);
	}
}