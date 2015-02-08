using UnityEngine;
using System.Collections;

public class GameMenu : UIMenu
{
	public UIButton singleModeButton;
	public UIButton multiModeButton;
	public UIButton gameConfigButton;

	public UIButton backButton;

	public void init()
	{
		singleModeButton.onClick += onSingleModeButtonClick;
		multiModeButton.onClick += onMultiModeButtonClick;
		gameConfigButton.onClick += onGameConfigButtonClick;

		backButton.onClick += onBackButtonClick;
	}

	public override void setEnabled (bool isEnabled)
	{
		
	}

	private void onSingleModeButtonClick()
	{
		//Create config for player
		PlayerInput inputController = new PlayerInput(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow);
		PlayerConfig playerConfig = new PlayerConfig(PlayerIndex.P1, PlayerColor.P1, inputController);

		//Create config for Single Player Mode
		AsteroidsGameConfig.playerConfigList.Add(playerConfig);
		AsteroidsGameConfig.gameMode = GameModes.SinglePlayerMode;

		Application.LoadLevel("Game");
	}

	private void onMultiModeButtonClick()
	{
		App.instance.showMultiPlayerModeMenu();
	}

	private void onGameConfigButtonClick()
	{
		App.instance.showGameConfigMenu();
	}

	private void onBackButtonClick()
	{
		App.instance.back();
	}
}