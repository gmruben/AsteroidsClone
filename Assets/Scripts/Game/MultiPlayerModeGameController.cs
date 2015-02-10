using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class has the logic for the multi player game mode. It updates the scores and lives of the different players and
/// dispatches game flow related events.
/// </summary>
public class MultiPlayerModeGameController : GameModeController
{
	private Game game;
	private List<Player> playerList = new List<Player>();

	private Vector3[] spawnPointList;

	private MultiPlayerGameHUD gameHUD;
	private MultiPlayerGameOverMenu gameOverMenu;

	public MultiPlayerModeGameController(Game game, Vector3[] spawnPointList, List<PlayerConfig> playerConfigList)
	{
		this.game = game;
		this.spawnPointList = spawnPointList;

		gameHUD = MenuManager.instantiateMultiPlayerGameHUD();
		gameHUD.pauseButton.onClick += onPauseButtonClick;

		int playerNumLives = GameParamConfig.instance.retrieveParamValue<int>(GameConfigParamIds.PlayerNumLives);

		//Initialize all the players
		for (int i = 0; i < playerConfigList.Count; i++)
		{
			Player player = EntityManager.instantiatePlayer();
			playerList.Add(player);

			player.init(this, playerConfigList[i]);
			player.reset(spawnPointList[player.index], playerNumLives);
			
			player.onDead += onPlayerDead;
		}
	}

	public override void setActive(bool isActive)
	{
		gameHUD.setEnabled(isActive);
	}

	public override void updateScore(Player player)
	{
		gameHUD.updateScore(player.index, player.score);
	}
	
	public override void updateLives(Player player)
	{
		gameHUD.updateLives(player.index, player.numLives);
	}

	public override void reset()
	{
		//Respawn all the players
		int playerNumLives = GameParamConfig.instance.retrieveParamValue<int>(GameConfigParamIds.PlayerNumLives);
		for (int i = 0; i < playerList.Count; i++)
		{
			playerList[i].reset(spawnPointList[playerList[i].index], playerNumLives);
		}
	}

	//Called whenever a player dies
	private void onPlayerDead()
	{
		//Check if all the players are dead
		if( allPlayersAreDead())
		{
			//Pause the game
			game.setGamePause(true);

			//Check if we have a new highscore
			int score = getHighestScore();
			if (score > GameSaveManager.instance.gameSave.highscore)
			{
				GameSaveManager.instance.gameSave.highscore = score;
				GameSaveManager.instance.saveData();
			}

			//We use a coroutine to delay the Game Over Menu a bit after the death
			game.StartCoroutine(showGameOverMenu());

			MessageBus.dispatchGameEnd();
		}
	}

	/// <summary>
	/// Checks whether all the players in the game are dead or not
	/// </summary>
	/// <returns><c>true</c>, if all players are dead, <c>false</c> otherwise.</returns>
	private bool allPlayersAreDead()
	{
		for (int i = 0; i < playerList.Count; i++)
		{
			if (!playerList[i].isDead) return false;
		}
		return true;
	}

	/// <summary>
	/// Returns the highest score among the players
	/// </summary>
	/// <returns>The highest score.</returns>
	private int getHighestScore()
	{
		int score = -1;
		for (int i = 0; i < playerList.Count; i++)
		{
			if (playerList[i].score > score)
			{
				score = playerList[i].score;
			}
		}
		return score;
	}

	private void onPauseButtonClick()
	{
		dispatchOnGamePause();
	}

	private IEnumerator showGameOverMenu()
	{
		yield return new WaitForSeconds(0.5f);
		
		gameOverMenu = MenuManager.instantiateMultiPlayerGameOverMenu();
		gameOverMenu.init(playerList);
		
		gameOverMenu.retryButton.onClick += onRetryButtonClick;
		gameOverMenu.mainMenuButton.onClick += onMainMenuButtonClick;
	}
	
	private void onRetryButtonClick()
	{
		gameOverMenu.retryButton.onClick -= onRetryButtonClick;
		gameOverMenu.mainMenuButton.onClick -= onMainMenuButtonClick;

		GameObject.Destroy(gameOverMenu.gameObject);

		reset();
		dispatchOnGameRestart();
	}
	
	private void onMainMenuButtonClick()
	{
		gameOverMenu.retryButton.onClick -= onRetryButtonClick;
		gameOverMenu.mainMenuButton.onClick -= onMainMenuButtonClick;

		GameObject.Destroy(gameOverMenu.gameObject);

		dispatchOnGameEnd();
	}
}