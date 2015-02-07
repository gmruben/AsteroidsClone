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

	private void onSingleModeButtonClick()
	{
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