using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

/// <summary>
/// This is where the main game occurs.
/// </summary>
public class Game : MonoBehaviour
{
	//Spawn points for single and multi player modes
	public Transform singlePlayerSpawnPoint;
	public Transform[] multiPlayerSpawnPointList;

	public AsteroidManager asteroidManager;
	public PowerUpManager powerUpManager;

	private PauseMenu pauseMenu;

	private bool isActive = true;

	//List with all the entities the game needs to update
	private List<IUpdateable> updatableItemList = new List<IUpdateable>();

	//This object controls the specific logic for each game mode
	private GameModeController gameModeController;

	void Awake()
	{
		//If there is no game config, create one (only for development: just in case we started the game from "Game" scene)
		if (AsteroidsGameConfig.playerConfigList.Count == 0)
		{
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
				//updatableItemList[i].update(Time.deltaTime);
			}
		}
	}

	public void init()
	{
		//Instantiate the game controller depending on what game mode we are playing
		if (AsteroidsGameConfig.gameMode == GameModes.SinglePlayerMode)
		{
			gameModeController = new SinglePlayerModeGameController(this, singlePlayerSpawnPoint.position, AsteroidsGameConfig.playerConfigList[0]);
		}
		else if (AsteroidsGameConfig.gameMode == GameModes.MultiPlayerMode)
		{
			Vector3[] spawnPointList = multiPlayerSpawnPointList.Select(t => t.position).ToArray();
			gameModeController = new MultiPlayerModeGameController(this, spawnPointList, AsteroidsGameConfig.playerConfigList);
		}

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
		PoolManager.instance.createPool (PoolIds.Bullet, Resources.Load("Prefabs/Bullet") as GameObject, 50);
		PoolManager.instance.createPool (PoolIds.HeavyBullet, Resources.Load("Prefabs/HeavyBullet") as GameObject, 50);
		PoolManager.instance.createPool (PoolIds.Particle, Resources.Load("Prefabs/Particle") as GameObject, 500);

		PoolManager.instance.createPool (PoolIds.AsteroidBig, Resources.Load("Prefabs/Asteroid_Big") as GameObject, 50);
		PoolManager.instance.createPool (PoolIds.AsteroidMedium, Resources.Load("Prefabs/Asteroid_Medium") as GameObject, 50);
		PoolManager.instance.createPool (PoolIds.AsteroidSmall, Resources.Load("Prefabs/Asteroid_Small") as GameObject, 50);
	}
	
	public void setGamePause(bool isPause)
	{
		isActive = !isPause;
		MessageBus.dispatchGamePause(isPause);
		
		gameModeController.setActive(!isPause);
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
		//Clean the message bus
		MessageBus.clean ();

		Application.LoadLevel("Menus");
	}

	private void onGamePause()
	{
		setGamePause(true);

		pauseMenu = MenuManager.instantiatePauseMenu();
		pauseMenu.init();

		addPauseListeners();
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
		removePauseListeners();
		GameObject.Destroy(pauseMenu.gameObject);

		setGamePause(false);
	}

	private void onRestartButtonClick()
	{
		removePauseListeners();
		GameObject.Destroy(pauseMenu.gameObject);
		
		restartGame();
		setGamePause(false);

		gameModeController.reset();
	}

	private void onPauseMainMenuButtonClick()
	{
		removePauseListeners();
		GameObject.Destroy(pauseMenu.gameObject);
		
		returnToMainMenu();
	}

	private void addPauseListeners()
	{
		pauseMenu.resumeButton.onClick += onResumeButtonClick;
		pauseMenu.restartButton.onClick += onRestartButtonClick;
		pauseMenu.mainMenuButton.onClick += onPauseMainMenuButtonClick;
	}

	private void removePauseListeners()
	{
		pauseMenu.resumeButton.onClick -= onResumeButtonClick;
		pauseMenu.restartButton.onClick -= onRestartButtonClick;
		pauseMenu.mainMenuButton.onClick -= onPauseMainMenuButtonClick;
	}
}