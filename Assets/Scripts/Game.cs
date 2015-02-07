using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public GameCamera gameCamera;

	public GameHUD gameHUD;
	public Player player;

	public AsteroidManager asteroidManager;

	private GameOverMenu gameOverMenu;

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

		player.onDead += onPlayerDead;

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

	private void onPlayerDead()
	{
		MessageBus.dispatchGamePause(true);

		//We use a coroutine to delay the Game Over Menu a bit after the death
		StartCoroutine(showGameOverMenu());
	}

	private IEnumerator showGameOverMenu()
	{
		yield return new WaitForSeconds(0.5f);

		gameOverMenu = MenuManager.instantiateGameOverMenu();
		gameOverMenu.init();
		
		gameOverMenu.retryButton.onClick += onRetryButtonClick;
		gameOverMenu.mainMenuButton.onClick += onMainMenuButtonClick;
	}

	private void onRetryButtonClick()
	{
		GameObject.Destroy(gameOverMenu.gameObject);
		gameOverMenu.retryButton.onClick += onRetryButtonClick;

		//RETRY
	}

	private void onMainMenuButtonClick()
	{
		GameObject.Destroy(gameOverMenu.gameObject);
		gameOverMenu.retryButton.onClick += onRetryButtonClick;

		//Remove all the pools of objects
		PoolManager.instance.removePool("bullet");
		PoolManager.instance.removePool("heavyBullet");
		PoolManager.instance.removePool("asteroid");
		PoolManager.instance.removePool("particle");
	}
}