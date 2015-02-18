using UnityEngine;
using System.Collections;

public class Asteroid_Medium : Asteroid
{
	public override void initParams()
	{
		//Get the config parameters for the medium asteroids
		score = GameParamConfig.instance.retrieveParamValue<int>(GameConfigParamIds.AsteroidMediumScore);
		speed = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.AsteroidMediumSpeed);
	}

	public override void hit(Bullet bullet, Vector3 position, Vector3 direction)
	{
		if (bullet.shooter is Player)
		{
			Player other = bullet.shooter as Player;
			other.addScore(score);

			customParticleEmitter.hit(other.color, position, -direction);

			//When a medium asteroid is hit, it creates two small asteroids
			Vector3 direction1 = Quaternion.AngleAxis(90, Vector3.back) * direction;
			Vector3 direction2 = Quaternion.AngleAxis(-90, Vector3.back) * direction;

			asteroidManager.instantiateAsteroid(PoolManager.PoolIds.AsteroidSmall, position, direction1);
			asteroidManager.instantiateAsteroid(PoolManager.PoolIds.AsteroidSmall, position, direction2);

			PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
			PoolManager.instance.destroyInstance(bullet.GetComponent<PoolInstance>());

			GameCamera.instance.shake(0.25f, 0.25f);
		}
	}
}