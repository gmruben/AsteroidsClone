using UnityEngine;
using System.Collections;

public class Asteroid_Big : Asteroid
{
	public override void initParams()
	{
		//Get the config parameters for the big asteroids
		score = GameParamConfig.instance.retrieveParamValue<int>(GameConfigParamIds.AsteroidBigScore);
		speed = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.AsteroidBigSpeed);
	}

	public override void hit(Vector3 position, Vector3 direction)
	{
		//When a big asteroid is hit, it creates four medium asteroids
		Vector3 direction1 = Quaternion.AngleAxis(45, Vector3.back) * direction;
		Vector3 direction2 = Quaternion.AngleAxis(90, Vector3.back) * direction;
		Vector3 direction3 = Quaternion.AngleAxis(-45, Vector3.back) * direction;
		Vector3 direction4 = Quaternion.AngleAxis(-90, Vector3.back) * direction;

		asteroidManager.instantiateAsteroid(PoolIds.AsteroidMedium, position, direction1);
		asteroidManager.instantiateAsteroid(PoolIds.AsteroidMedium, position, direction2);
		asteroidManager.instantiateAsteroid(PoolIds.AsteroidMedium, position, direction3);
		asteroidManager.instantiateAsteroid(PoolIds.AsteroidMedium, position, direction4);

		PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
	}
}