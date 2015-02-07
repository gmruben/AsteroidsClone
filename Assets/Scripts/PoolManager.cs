using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PoolManager : MonoBehaviour
{
	private static PoolManager _instance;

	private static Dictionary<string, PoolData> poolList = new Dictionary<string, PoolData> ();
	
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

	public void removePool(string poolId)
	{
		PoolData poolData = poolList[poolId];
		for (int i = 0; i < poolData.numInstances; i++)
		{
			GameObject.Destroy(poolData.instanceList[i].gameObject);
		}
	}

	public PoolInstance retrievePoolInstance(string poolId)
	{
		PoolData poolData = poolList[poolId];

		//Start from the last instance index and iterate through all the instances
		for (int i = poolData.instanceIndex; i < poolData.instanceIndex + poolData.numInstances; i++)
		{
			int index = (i < poolData.numInstances) ? i : i % poolData.numInstances;
			if (!poolData.instanceList[index].isAlive)
			{
				//Revive the instance and store the index as the last instance used
				poolData.instanceList[index].revive();
				poolData.instanceIndex = index;

				return poolData.instanceList[index];
			}
		}

		Debug.LogWarning("There are no instances availables in " + poolId);
		return null;
	}

	public void destroyInstance(PoolInstance instance)
	{
		instance.kill();
	}

	private class PoolData
	{
		public int numInstances;
		public int instanceIndex = 0;

		public PoolInstance[] instanceList;
	}
}