using UnityEngine;
using System.Collections;

public class EntityManager
{
	public static Bullet instantiateBullet()
	{
		return (GameObject.Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject).GetComponent<Bullet>();
	}

	public static Bullet instantiateHeavyBullet()
	{
		return (GameObject.Instantiate(Resources.Load("Prefabs/HeavyBullet")) as GameObject).GetComponent<Bullet>();
	}

	public static CustomParticle instantiateParticle()
	{
		return (GameObject.Instantiate(Resources.Load("Prefabs/Particle")) as GameObject).GetComponent<CustomParticle>();
	}
}