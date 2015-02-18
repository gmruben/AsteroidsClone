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

	public override void hit(Bullet bullet, Vector3 position, Vector3 direction)
	{
		if (bullet.shooter is Player)
		{
			Player other = bullet.shooter as Player;
			other.addScore(score);

			customParticleEmitter.hit(other.color, position, -direction);

			//When a big asteroid is hit, it creates four medium asteroids
			Vector3 direction1 = Quaternion.AngleAxis(45, Vector3.back) * direction;
			Vector3 direction2 = Quaternion.AngleAxis(90, Vector3.back) * direction;
			Vector3 direction3 = Quaternion.AngleAxis(-45, Vector3.back) * direction;
			Vector3 direction4 = Quaternion.AngleAxis(-90, Vector3.back) * direction;

			asteroidManager.instantiateAsteroid(PoolManager.PoolIds.AsteroidMedium, position, direction1);
			asteroidManager.instantiateAsteroid(PoolManager.PoolIds.AsteroidMedium, position, direction2);
			asteroidManager.instantiateAsteroid(PoolManager.PoolIds.AsteroidMedium, position, direction3);
			asteroidManager.instantiateAsteroid(PoolManager.PoolIds.AsteroidMedium, position, direction4);

			PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
			PoolManager.instance.destroyInstance(bullet.GetComponent<PoolInstance>());

			GameCamera.instance.shake(0.50f, 0.50f);
		}
	}
}