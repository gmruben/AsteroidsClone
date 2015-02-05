using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PoolManager : MonoBehaviour
{
	private const int numBullets = 50;

	private PoolInstance[] bulletList;
	private static PoolManager _instance;

	private static Dictionary<string, PoolData> poolList;
	
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

		//Initialize pool dictionary
		poolList = new Dictionary<string, PoolData> ();
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


	public void createPool(string poolId, GameObject prefab, int numInstances)
	{
		PoolData poolData = new PoolData ();

		poolData.numInstances = numInstances;
		poolData.instanceList = new PoolInstance[numInstances];

		for (int i = 0; i < numInstances; i++)
		{
			//The pool only operates with PoolInstance objects
			PoolInstance instance = (GameObject.Instantiate(prefab) as GameObject).GetComponent<PoolInstance>();
			
			instance.init();
			instance.transform.parent = transform;
			
			poolData.instanceList[i] = instance;
		}

		poolList.Add (poolId, poolData);
	}

	public PoolInstance retrievePoolInstance(string poolId)
	{
		PoolData poolData = poolList[poolId];

		for (int i = 0; i < poolData.numInstances; i++)
		{
			if (!poolData.instanceList[i].isAlive)
			{
				poolData.instanceList[i].revive();
				return poolData.instanceList[i];
			}
		}

		Debug.LogWarning("There are no instances availables in " + poolId);
		return null;
	}

	private class PoolData
	{
		public int numInstances;
		public PoolInstance[] instanceList;
	}
}