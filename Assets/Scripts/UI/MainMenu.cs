using UnityEngine;
using System.Collections;

public class MainMenu : UIMenu
{
	public UIButton playButton;
	public UIButton quitButton;

	public void init()
	{
		playButton.onClick += onPlayButtonClick;
		quitButton.onClick += onQuitButtonClick;
	}

	private void onPlayButtonClick()
	{
		Application.LoadLevel ("Game");
	}

	private void onQuitButtonClick()
	{
		Application.Quit ();
	}
}