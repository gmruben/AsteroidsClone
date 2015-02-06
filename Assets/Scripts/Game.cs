using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public GameCamera gameCamera;

	public GameHUD gameHUD;
	public Player player;

	public AsteroidManager asteroidManager;

	void Awake()
	{
		init ();
	}

	void Update()
	{
		asteroidManager.update(Time.deltaTime);
	}

	public void init()
	{
		player.init(this);
		gameHUD.init();

		asteroidManager.init(gameCamera, new LevelConfig());
		asteroidManager.onEnd += onAsteroidManagerEnd;

		//Create pool instances for all the items that are going to be instantiated intensively
		PoolManager.instance.createPool ("bullet", Resources.Load("Prefabs/Bullet") as GameObject, 50);
		PoolManager.instance.createPool ("heavyBullet", Resources.Load("Prefabs/HeavyBullet") as GameObject, 50);
		PoolManager.instance.createPool ("asteroid", Resources.Load("Prefabs/Asteroid") as GameObject, 50);
		PoolManager.instance.createPool ("particle", Resources.Load("Prefabs/Particle") as GameObject, 150);
	}

	public void updateScore(int playerIndex, int score)
	{
		gameHUD.updateScore(score);
	}

	private void onAsteroidManagerEnd()
	{
		Debug.Log("YAY!");
	}
}