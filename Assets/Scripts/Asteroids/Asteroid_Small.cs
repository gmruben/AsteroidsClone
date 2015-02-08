using UnityEngine;
using System.Collections;

public class Asteroid_Small : Asteroid
{
	public override void initParams()
	{
		score = GameConfig.instance.retrieveParamValue<int>(GameConfigParamIds.AsteroidSmallScore);
		speed = GameConfig.instance.retrieveParamValue<float>(GameConfigParamIds.AsteroidSmallSpeed);
	}

	public override void hit(Vector3 position, Vector3 direction)
	{
		kill ();
	}

	public override void kill()
	{
		PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());

		CustomParticleEmitter customParticleEmitter = new CustomParticleEmitter();
		customParticleEmitter.explode2(Color.white, cachedTransform.position);
	}
}