using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour
{
	private const int numBullets = 50;

	private PoolInstance[] bulletList;
	private static PoolManager _instance;
	
	public static PoolManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameObject("PoolManager").AddComponent<PoolManager>();
			}
			
			return _instance;
		}
	}

	public void init()
	{
		GameObject bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");

		//Create the pool for the bullets
		bulletList = new PoolInstance[numBullets]; 
		for (int i = 0; i < numBullets; i++)
		{
			//The pool only operates with PoolInstance objects
			PoolInstance bullet = (GameObject.Instantiate(bulletPrefab) as GameObject).GetComponent<PoolInstance>();

			bullet.init();
			bullet.transform.parent = transform;

			bulletList[i] = bullet;
		}
	}

	public Bullet getBulletInstance()
	{
		for (int i = 0; i < numBullets; i++)
		{
			if (!bulletList[i].isAlive)
			{
				//When getting the actual object, we return the specific component (could be a generic if we needed different components)
				bulletList[i].revive();
				return bulletList[i].GetComponent<Bullet>();
			}
		}

		Debug.LogWarning("THERE ARE NO BULLET INSTANCES AVAILABLE");
		return null;
	}
}