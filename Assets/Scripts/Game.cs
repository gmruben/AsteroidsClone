using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public GameHUD gameHUD;
	public Player player;

	void Awake()
	{
		init ();
	}

	public void init()
	{
		player.init(this);
		gameHUD.init();

		//Create pool instances for all the items that are going to be instantiated intensively
		GameObject bulletPrefab = GameObject.Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject;
		PoolManager.instance.createPool ("bullet", bulletPrefab, 50);

		GameObject heavyBulletPrefab = GameObject.Instantiate(Resources.Load("Prefabs/HeavyBullet")) as GameObject;
		PoolManager.instance.createPool ("heavyBullet", heavyBulletPrefab, 50);

		GameObject particlePrefab = GameObject.Instantiate(Resources.Load("Prefabs/Particle")) as GameObject;
		PoolManager.instance.createPool ("particle", particlePrefab, 150);
	}

	public void updateScore(int playerIndex, int score)
	{
		gameHUD.updateScore(score);
	}
}