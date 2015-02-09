using UnityEngine;
using System.Collections;

/// <summary>
/// This class have functions for instantiating all the entities in the game. If controls that entities
/// that are instantiated intensively are retrieved from a pool.
/// </summary>
public class EntityManager
{
	public static Player instantiatePlayer()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Player");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<Player>();
	}

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

	public static Asteroid instantiateAsteroid(string id)
	{
		//We have a different pool for each type of asteroid
		return PoolManager.instance.retrievePoolInstance (id).GetComponent<Asteroid>();
	}

	public static PowerUp instantiatePowerUp(string id)
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/PowerUp_" + id);
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<PowerUp>();
	}
}