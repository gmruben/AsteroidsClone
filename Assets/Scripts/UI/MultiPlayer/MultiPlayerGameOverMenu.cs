using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MultiPlayerGameOverMenu : UIMenu
{
	public Text highscore;
	public Text[] scoreList;

	public UIButton retryButton;
	public UIButton mainMenuButton;

	public void init(List<Player> playerList)
	{
		highscore.text = "HIGH SCORE: " + GameSaveManager.instance.gameSave.highscore;

		for (int i = 0; i < playerList.Count; i++)
		{
			int index = playerList[i].index;
			scoreList[index].text = "P" + (index + 1) +  " SCORE: " + playerList[i].score;
		}
	}

	public override void setEnabled (bool isEnabled)
	{
		
	}
}