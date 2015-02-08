using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MultiPlayerModeGameController : GameModeController
{
	private Game game;
	private List<Player> playerList = new List<Player>();

	private MultiPlayerGameHUD gameHUD;
	private MultiPlayerGameOverMenu gameOverMenu;

	public MultiPlayerModeGameController(Game game, List<PlayerConfig> playerConfigList)
	{
		this.game = game;

		gameHUD = MenuManager.instantiateMultiPlayerGameHUD();
		gameHUD.pauseButton.onClick += onPauseButtonClick;

		int playerNumLifes = GameConfig.instance.retrieveParamValue<int>(GameConfigParamIds.PlayerNumLifes);

		for (int i = 0; i < playerConfigList.Count; i++)
		{
			Player player = EntityManager.instantiatePlayer();
			playerList.Add(player);

			player.init(this, playerConfigList[i]);
			player.reset(playerNumLifes);
			
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
		int playerNumLifes = GameConfig.instance.retrieveParamValue<int>(GameConfigParamIds.PlayerNumLifes);
		for (int i = 0; i < playerList.Count; i++)
		{
			playerList[i].reset(playerNumLifes);
		}
	}

	private void onPlayerDead()
	{
		//Check if all the players are dead
		if( allPlayersAreDead())
		{
			//Pause the game
			game.setGamePause(true);
			
			//We use a coroutine to delay the Game Over Menu a bit after the death
			game.StartCoroutine(showGameOverMenu());
		}
	}

	private bool allPlayersAreDead()
	{
		for (int i = 0; i < playerList.Count; i++)
		{
			if (!playerList[i].isDead) return false;
		}
		return true;
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