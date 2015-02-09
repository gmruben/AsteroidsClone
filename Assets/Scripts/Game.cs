using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is where the main game occurs.
/// </summary>
public class Game : MonoBehaviour
{
	public AsteroidManager asteroidManager;
	public PowerUpManager powerUpManager;

	private PauseMenu pauseMenu;

	private bool isActive = true;

	private List<IUpdateable> updatableItemList = new List<IUpdateable>();

	private GameModeController gameModeController;

	void Awake()
	{
		//If there is no game config, create one (just in case we started the game from "Game" scene)
		if (AsteroidsGameConfig.playerConfigList.Count == 0)
		{
			GameSaveManager.loadData();

			//Create config for player
			InputController inputController = new InputController(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow);
			PlayerConfig playerConfig = new PlayerConfig(PlayerIndex.P1, Color.white, inputController);
			
			//Create config for Single Player Mode Game
			AsteroidsGameConfig.playerConfigList.Clear();
			AsteroidsGameConfig.playerConfigList.Add(playerConfig);

			AsteroidsGameConfig.gameMode = GameModes.SinglePlayerMode;
		}

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
	
		asteroidManager.init();
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

	private void restartGame()
	{
		asteroidManager.clear();
		powerUpManager.clear();

		setGamePause(false);
	}

	private void returnToMainMenu()
	{
		//Clear the pool manager
		PoolManager.instance.clearPoolList();
		Application.LoadLevel("Menus");
	}

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