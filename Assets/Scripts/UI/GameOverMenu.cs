using UnityEngine;
using System.Collections;

public class GameOverMenu : UIMenu
{
	public UIButton retryButton;
	public UIButton mainMenuButton;

	public void init()
	{
		retryButton.onClick += onRetryButtonClick;
		mainMenuButton.onClick += onMainMenuButtonClick;
	}

	private void onRetryButtonClick()
	{

	}

	private void onMainMenuButtonClick()
	{
		Application.LoadLevel("Menus");
	}
}