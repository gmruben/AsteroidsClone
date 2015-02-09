using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiPlayerModeMenu : UIMenu
{
	private const int numPlayers = 4;

	public MultiPlayerBox[] playerBoxList;

	public MultiPlayerBox p1Box;
	public MultiPlayerBox p2Box;
	public MultiPlayerBox p3Box;
	public MultiPlayerBox p4Box;

	public UIButton playButton;
	public UIButton backButton;

	private PlayerConfig[] playerConfigList;

	private int numReady = 0;

	public void init()
	{
		playerConfigList = new PlayerConfig[numPlayers];

		playerBoxList = new MultiPlayerBox[numPlayers];

		playerConfigList[PlayerIndex.P1] = new PlayerConfig(PlayerIndex.P1, PlayerColor.P1, new InputController(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow));
		playerConfigList[PlayerIndex.P2] = new PlayerConfig(PlayerIndex.P2, PlayerColor.P2, new InputController(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S));
		playerConfigList[PlayerIndex.P3] = new PlayerConfig(PlayerIndex.P3, PlayerColor.P3, new InputController(KeyCode.F, KeyCode.H, KeyCode.T, KeyCode.G));
		playerConfigList[PlayerIndex.P4] = new PlayerConfig(PlayerIndex.P4, PlayerColor.P4, new InputController(KeyCode.J, KeyCode.L, KeyCode.I, KeyCode.K));

		p1Box.init(playerConfigList[PlayerIndex.P1]);
		p2Box.init(playerConfigList[PlayerIndex.P2]);
		p3Box.init(playerConfigList[PlayerIndex.P3]);
		p4Box.init(playerConfigList[PlayerIndex.P4]);

		p1Box.onReady += onReady;
		p2Box.onReady += onReady;
		p3Box.onReady += onReady;
		p4Box.onReady += onReady;

		playerBoxList[0] = p1Box;
		playerBoxList[1] = p2Box;
		playerBoxList[2] = p3Box;
		playerBoxList[3] = p4Box;

		playButton.onClick += onPlayButtonClick;
		backButton.onClick += onBackButtonClick;

		//Set button inactive until there is at least one player ready
		playButton.setActive(false);
	}

	public override void setEnabled (bool isEnabled)
	{
		
	}

	private void onReady(int playerIndex, bool isReady)
	{
		if (isReady) numReady++;
		else numReady--;

		//We need at least 2 player to play multiplayer
		playButton.setActive(numReady > 1);
	}
	
	private void startGame()
	{
		AsteroidsGameConfig.playerConfigList.Clear();
		for (int i = 0; i < playerBoxList.Length; i++)
		{
			if (playerBoxList[i].isReady)
			{
				//If the player is ready, add its config to the list
				AsteroidsGameConfig.playerConfigList.Add(playerConfigList[i]);
			}
		}

		AsteroidsGameConfig.gameMode = GameModes.MultiPlayerMode;

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