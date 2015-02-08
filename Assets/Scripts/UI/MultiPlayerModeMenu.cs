using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiPlayerModeMenu : UIMenu
{
	private const int numPlayers = 4;

	public MultiPlayerBox p1Box;
	public MultiPlayerBox p2Box;
	public MultiPlayerBox p3Box;
	public MultiPlayerBox p4Box;

	public UIButton playButton;
	public UIButton backButton;

	private PlayerConfig[] playerConfigList;

	public void init()
	{
		playerConfigList = new PlayerConfig[numPlayers];

		playerConfigList[PlayerIndex.P1] = new PlayerConfig(PlayerIndex.P1, new PlayerInput(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow));
		playerConfigList[PlayerIndex.P2] = new PlayerConfig(PlayerIndex.P2, new PlayerInput(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S));
		playerConfigList[PlayerIndex.P3] = new PlayerConfig(PlayerIndex.P3, new PlayerInput(KeyCode.F, KeyCode.H, KeyCode.T, KeyCode.G));
		playerConfigList[PlayerIndex.P4] = new PlayerConfig(PlayerIndex.P4, new PlayerInput(KeyCode.J, KeyCode.L, KeyCode.I, KeyCode.K));

		p1Box.init(playerConfigList[PlayerIndex.P1]);
		p2Box.init(playerConfigList[PlayerIndex.P2]);
		p3Box.init(playerConfigList[PlayerIndex.P3]);
		p4Box.init(playerConfigList[PlayerIndex.P4]);

		p1Box.onReady += onReady;
		p2Box.onReady += onReady;
		p3Box.onReady += onReady;
		p4Box.onReady += onReady;

		playButton.onClick += onPlayButtonClick;
		backButton.onClick += onBackButtonClick;

		//Set button inactive until there is at least one player ready
		playButton.setActive(false);
	}

	private void onReady(int playerIndex, bool isReady)
	{

	}


	private void startGame()
	{
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