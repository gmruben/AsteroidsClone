using UnityEngine;
using System.Collections;

public class EntityManager
{
	public static Bullet instantiateBullet()
	{
		return PoolManager.instance.retrievePoolInstance ("bullet").GetComponent<Bullet>();
		//return (GameObject.Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject).GetComponent<Bullet>();
	}

	public static Bullet instantiateHeavyBullet()
	{
		return PoolManager.instance.retrievePoolInstance ("heavyBullet").GetComponent<Bullet>();
		//return (GameObject.Instantiate(Resources.Load("Prefabs/HeavyBullet")) as GameObject).GetComponent<Bullet>();
	}

	public static CustomParticle instantiateParticle()
	{
		return PoolManager.instance.retrievePoolInstance ("particle").GetComponent<CustomParticle>();
		//return (GameObject.Instantiate(Resources.Load("Prefabs/Particle")) as GameObject).GetComponent<CustomParticle>();
	}
}