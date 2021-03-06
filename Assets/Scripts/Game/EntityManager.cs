﻿using UnityEngine;
using System.Collections;
 
/// <summary>
/// This class have functions for instantiating all the entities in the game. It controls that entities
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
		return PoolManager.instance.retrievePoolInstance (PoolManager.PoolIds.Bullet).GetComponent<Bullet>();
	}

	public static Bullet instantiateHeavyBullet()
	{
		return PoolManager.instance.retrievePoolInstance (PoolManager.PoolIds.HeavyBullet).GetComponent<Bullet>();
	}

	public static CustomParticle instantiateParticle()
	{
		return PoolManager.instance.retrievePoolInstance (PoolManager.PoolIds.Particle).GetComponent<CustomParticle>();
	}

	public static Asteroid instantiateAsteroid(PoolManager.PoolIds id)
	{
		//We have a different pool for each type of asteroid
		return PoolManager.instance.retrievePoolInstance (id).GetComponent<Asteroid>();
	}

	public static Ship instantiateShip()
	{
		//We have a different pool for each type of asteroid
		return PoolManager.instance.retrievePoolInstance (PoolManager.PoolIds.Ship).GetComponent<Ship>();
	}

	public static PowerUp instantiatePowerUp(string id)
	{
		//Not many power ups are instantiated, so we dont need to create a pool for them
		GameObject resource = Resources.Load<GameObject>("Prefabs/PowerUp_" + id);
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<PowerUp>();
	}
}