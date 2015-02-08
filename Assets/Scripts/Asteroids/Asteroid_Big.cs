using UnityEngine;
using System.Collections;

public class Asteroid_Big : Asteroid
{
	public override void initParams()
	{
		//Get the config parameters for the big asteroids
		score = GameConfig.instance.retrieveParamValue<int>(GameConfigParamIds.AsteroidBigScore);
		speed = GameConfig.instance.retrieveParamValue<float>(GameConfigParamIds.AsteroidBigSpeed);
	}

	public override void hit(Vector3 position, Vector3 direction)
	{
		//When a big asteroid is hit, it creates four medium asteroids
		Vector3 direction1 = Quaternion.AngleAxis(45, Vector3.back) * direction;
		Vector3 direction2 = Quaternion.AngleAxis(90, Vector3.back) * direction;
		Vector3 direction3 = Quaternion.AngleAxis(-45, Vector3.back) * direction;
		Vector3 direction4 = Quaternion.AngleAxis(-90, Vector3.back) * direction;

		asteroidManager.instantiateAsteroid("asteroid_medium", position, direction1);
		asteroidManager.instantiateAsteroid("asteroid_medium", position, direction2);
		asteroidManager.instantiateAsteroid("asteroid_medium", position, direction3);
		asteroidManager.instantiateAsteroid("asteroid_medium", position, direction4);

		PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
	}

	public override void kill()
	{
		PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());

		CustomParticleEmitter customParticleEmitter = new CustomParticleEmitter();
		customParticleEmitter.explode2(Color.white, cachedTransform.position);
	}
}