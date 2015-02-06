using UnityEngine;
using System.Collections;

public class MultiPlayerModeMenu : UIMenu
{
	public UIButton playButton;
	public UIButton backButton;

	public void init()
	{
		playButton.onClick += onPlayButtonClick;
		backButton.onClick += onBackButtonClick;
	}

	private void onPlayButtonClick()
	{
		Application.LoadLevel("Game");
	}

	private void onBackButtonClick()
	{
		App.instance.back();
	}
}