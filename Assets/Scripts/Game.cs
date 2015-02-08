using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public AsteroidManager asteroidManager;
	public PowerUpManager powerUpManager;

	private PauseMenu pauseMenu;

	private bool isActive = true;

	private List<IUpdatable> updatableItemList = new List<IUpdatable>();

	private GameModeController gameModeController;

	void Awake()
	{
		init ();
	}

	void Update()
	{
		if (isActive)
		{
			//Update all the game objects
			for (int i = 0; i < updatableItemList.Count; i++)
			{
				updatableItemList[i].update(Time.deltaTime);
			}
		}
	}

	public void init()
	{
		//Instantiate the game controller depending on what game mode we are playing
		if (AsteroidsGameConfig.gameMode == GameModes.SinglePlayerMode) gameModeController = new SinglePlayerModeGameController(this, AsteroidsGameConfig.playerConfigList[0]);
		else if (AsteroidsGameConfig.gameMode == GameModes.MultiPlayerMode) gameModeController = new MultiPlayerModeGameController(this, AsteroidsGameConfig.playerConfigList);

		//Register for game flow events
		gameModeController.onGamePause += onGamePause;
		gameModeController.onGameRestart += onGameRestart;
		gameModeController.onGameEnd += onGameEnd;

		LevelConfig levelConfig = new LevelConfig();

		levelConfig.spawnTime = 5;
		levelConfig.numAsteroids = 3;

		asteroidManager.init(levelConfig);
		asteroidManager.onEnd += onAsteroidManagerEnd;

		powerUpManager.init();

		//Add all the updatable items to the list
		updatableItemList.Add(asteroidManager);
		updatableItemList.Add(powerUpManager);

		//Create pool instances for all the items that are going to be instantiated intensively
		PoolManager.instance.createPool ("bullet", Resources.Load("Prefabs/Bullet") as GameObject, 50);
		PoolManager.instance.createPool ("heavyBullet", Resources.Load("Prefabs/HeavyBullet") as GameObject, 50);
		PoolManager.instance.createPool ("particle", Resources.Load("Prefabs/Particle") as GameObject, 500);

		PoolManager.instance.createPool ("asteroid", Resources.Load("Prefabs/Asteroid") as GameObject, 50);
		PoolManager.instance.createPool ("asteroid_medium", Resources.Load("Prefabs/Asteroid_Medium") as GameObject, 50);
		PoolManager.instance.createPool ("asteroid_small", Resources.Load("Prefabs/Asteroid_Small") as GameObject, 50);
	}

	private void onAsteroidManagerEnd()
	{
		Debug.Log("YAY!");
	}

	private void restartGame()
	{
		asteroidManager.clear();
		setGamePause(false);
	}

	private void returnToMainMenu()
	{
		//Clear the pool manager
		PoolManager.instance.clearPoolList();
		Application.LoadLevel("Menus");
	}

	/*private IEnumerator showGameOverMenu()
	{
		yield return new WaitForSeconds(0.5f);

		gameOverMenu = MenuManager.instantiateGameOverMenu();
		gameOverMenu.init();
		
		gameOverMenu.retryButton.onClick += onRetryButtonClick;
		gameOverMenu.mainMenuButton.onClick += onMainMenuButtonClick;
	}*/

	private void onGamePause()
	{
		setGamePause(true);

		pauseMenu = MenuManager.instantiatePauseMenu();
		pauseMenu.init();

		pauseMenu.resumeButton.onClick += onResumeButtonClick;
		pauseMenu.restartButton.onClick += onRestartButtonClick;
		pauseMenu.mainMenuButton.onClick += onPauseMainMenuButtonClick;
	}

	private void onGameRestart()
	{
		restartGame();
	}

	private void onGameEnd()
	{
		returnToMainMenu();
	}

	private void onResumeButtonClick()
	{
		pauseMenu.resumeButton.onClick -= onResumeButtonClick;
		pauseMenu.restartButton.onClick -= onRestartButtonClick;
		pauseMenu.mainMenuButton.onClick -= onPauseMainMenuButtonClick;

		GameObject.Destroy(pauseMenu.gameObject);

		setGamePause(false);
	}

	private void onRestartButtonClick()
	{
		pauseMenu.resumeButton.onClick -= onResumeButtonClick;
		pauseMenu.restartButton.onClick -= onRestartButtonClick;
		pauseMenu.mainMenuButton.onClick -= onPauseMainMenuButtonClick;

		GameObject.Destroy(pauseMenu.gameObject);
		
		restartGame();
		setGamePause(false);

		gameModeController.reset();
	}

	private void onPauseMainMenuButtonClick()
	{
		pauseMenu.resumeButton.onClick -= onResumeButtonClick;
		pauseMenu.restartButton.onClick -= onRestartButtonClick;
		pauseMenu.mainMenuButton.onClick -= onPauseMainMenuButtonClick;

		GameObject.Destroy(pauseMenu.gameObject);
		
		returnToMainMenu();
	}

	public void setGamePause(bool isPause)
	{
		isActive = !isPause;
		MessageBus.dispatchGamePause(isPause);

		gameModeController.setActive(!isPause);
	}
}