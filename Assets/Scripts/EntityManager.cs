using UnityEngine;
using System.Collections;

public class EntityManager
{
	public static Bullet instantiateBullet()
	{
		return PoolManager.instance.retrievePoolInstance ("bullet").GetComponent<Bullet>();
	}

	public static Bullet instantiateHeavyBullet()
	{
		return PoolManager.instance.retrievePoolInstance ("heavyBullet").GetComponent<Bullet>();
	}

	public static CustomParticle instantiateParticle()
	{
		return PoolManager.instance.retrievePoolInstance ("particle").GetComponent<CustomParticle>();
	}

	public static Asteroid instantiateAsteroid()
	{
		return PoolManager.instance.retrievePoolInstance ("asteroid").GetComponent<Asteroid>();
	}
}