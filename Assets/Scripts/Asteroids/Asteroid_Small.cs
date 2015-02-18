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

	public override void hit(Bullet bullet, Vector3 position, Vector3 direction)
	{
		if (bullet.shooter is Player)
		{
			Player other = bullet.shooter as Player;
			other.addScore(score);

			customParticleEmitter.explosion(other.color, cachedTransform.position);

			//When a medium asteroid is hit, they dont create any more asteroids
			PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
			PoolManager.instance.destroyInstance(bullet.GetComponent<PoolInstance>());

			GameCamera.instance.shake(0.15f, 0.15f);
		}
	}
}