using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SinglePlayerModeMenu : UIMenu
{
	public UIButton playButton;
	public UIButton backButton;

	public void init()
	{
		playButton.onClick += onPlayButtonClick;
		backButton.onClick += onBackButtonClick;
	}

	public override void setEnabled (bool isEnabled)
	{
		
	}

	private void startGame()
	{
		//Create the configuration for the singleplayer mode

		//Create config for player
		InputController inputController = new InputController(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow);
		PlayerConfig playerConfig = new PlayerConfig(PlayerIndex.P1, Color.white, inputController);
		
		//Create config for Single Player Mode Game
		AsteroidsGameConfig.playerConfigList.Clear();
		AsteroidsGameConfig.playerConfigList.Add(playerConfig);

		AsteroidsGameConfig.gameMode = GameModes.SinglePlayerMode;

		Application.LoadLevel("Game");
	}

	private void onPlayButtonClick()
	{
		startGame();
	}

	private void onBackButtonClick()
	{
		App.instance.back();
	}
}