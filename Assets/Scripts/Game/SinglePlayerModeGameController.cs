using UnityEngine;
using System.Collections;

public class SinglePlayerModeGameController : GameModeController
{
	private Game game;
	private Player player;

	private SinglePlayerGameHUD gameHUD;
	private SinglePlayerGameOverMenu gameOverMenu;

	public SinglePlayerModeGameController(Game game, PlayerConfig playerConfig)
	{
		this.game = game;

		gameHUD = MenuManager.instantiateSinglePlayerGameHUD();
		gameHUD.pauseButton.onClick += onPauseButtonClick;

		int playerNumLives = GameParamConfig.instance.retrieveParamValue<int>(GameConfigParamIds.PlayerNumLives);
		player = EntityManager.instantiatePlayer();

		player.init(this, playerConfig);
		player.reset(playerNumLives);

		player.onDead += onPlayerDead;
	}

	public override void setActive(bool isActive)
	{
		gameHUD.setEnabled(isActive);
	}

	public override void updateScore(Player player)
	{
		gameHUD.updateScore(player.score);
	}
	
	public override void updateLives(Player player)
	{
		gameHUD.updateLives(player.numLives);
	}

	public override void reset()
	{
		int playerNumLives = GameParamConfig.instance.retrieveParamValue<int>(GameConfigParamIds.PlayerNumLives);
		player.reset(playerNumLives);
	}

	private void onPlayerDead()
	{
		//Pause the game
		game.setGamePause(true);

		//Check if we have a new highscore
		if (player.score > GameSaveManager.gameSave.highscore)
		{
			GameSaveManager.gameSave.highscore = player.score;
			GameSaveManager.saveData();
		}

		//We use a coroutine to delay the Game Over Menu a bit after the death
		game.StartCoroutine(showGameOverMenu());

		MessageBus.dispatchGameEnd();
	}

	private void onPauseButtonClick()
	{
		dispatchOnGamePause();
	}

	private IEnumerator showGameOverMenu()
	{
		yield return new WaitForSeconds(0.5f);
		
		gameOverMenu = MenuManager.instantiateSinglePlayerGameOverMenu();
		gameOverMenu.init(player);
		
		gameOverMenu.retryButton.onClick += onRetryButtonClick;
		gameOverMenu.mainMenuButton.onClick += onMainMenuButtonClick;
	}

	private void onRetryButtonClick()
	{
		gameOverMenu.retryButton.onClick -= onRetryButtonClick;
		GameObject.Destroy(gameOverMenu.gameObject);

		reset();
		dispatchOnGameRestart();
	}
	
	private void onMainMenuButtonClick()
	{
		GameObject.Destroy(gameOverMenu.gameObject);
		dispatchOnGameEnd();
	}
}