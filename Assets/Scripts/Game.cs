using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public GameHUD gameHUD;
	private Player player;

	public AsteroidManager asteroidManager;
	public PowerUpManager powerUpManager;

	private GameOverMenu gameOverMenu;
	private bool isActive = true;

	private int score;

	void Awake()
	{
		init ();
	}

	void Update()
	{
		if (isActive)
		{
			asteroidManager.update(Time.deltaTime);
			powerUpManager.update(Time.deltaTime);
		}
	}

	public void init()
	{
		gameHUD.init();

		player = EntityManager.instantiatePlayer();
		player.init(this);

		player.onDead += onPlayerDead;

		LevelConfig levelConfig = new LevelConfig();

		levelConfig.spawnTime = 5;
		levelConfig.numAsteroids = 3;

		asteroidManager.init(levelConfig);
		asteroidManager.onEnd += onAsteroidManagerEnd;

		powerUpManager.init();

		score = 0;

		//Create pool instances for all the items that are going to be instantiated intensively
		PoolManager.instance.createPool ("bullet", Resources.Load("Prefabs/Bullet") as GameObject, 50);
		PoolManager.instance.createPool ("heavyBullet", Resources.Load("Prefabs/HeavyBullet") as GameObject, 50);
		PoolManager.instance.createPool ("particle", Resources.Load("Prefabs/Particle") as GameObject, 500);

		PoolManager.instance.createPool ("asteroid", Resources.Load("Prefabs/Asteroid") as GameObject, 50);
		PoolManager.instance.createPool ("asteroid_medium", Resources.Load("Prefabs/Asteroid_Medium") as GameObject, 50);
		PoolManager.instance.createPool ("asteroid_small", Resources.Load("Prefabs/Asteroid_Small") as GameObject, 50);
	}

	public void updateScore(int playerIndex, int score)
	{
		gameHUD.updateScore(score);
	}

	public void updateLives(int playerIndex, int lives)
	{
		gameHUD.updateLives(lives);
	}

	private void onAsteroidManagerEnd()
	{
		Debug.Log("YAY!");
	}

	private void onPlayerDead()
	{
		isActive = false;
		MessageBus.dispatchGamePause(true);

		//We use a coroutine to delay the Game Over Menu a bit after the death
		StartCoroutine(showGameOverMenu());
	}

	private void endGame()
	{
		player.reset(Vector3.zero);
		asteroidManager.clear();

		isActive = true;
		MessageBus.dispatchGamePause(false);
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

		endGame();
	}

	private void onMainMenuButtonClick()
	{
		GameObject.Destroy(gameOverMenu.gameObject);
		gameOverMenu.retryButton.onClick += onRetryButtonClick;

		//Clear the pool manager
		PoolManager.instance.clearPoolList();
	}
}