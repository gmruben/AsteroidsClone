using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MultiPlayerGameHUD : UIMenu
{
	private const int numPlayers = 4;

	public Text[] scoreList;
	public Text[] livesList;

	public UIButton pauseButton;
	
	public override void setEnabled (bool isEnabled)
	{
		
	}

	public void updateScore(int playerIndex, int value)
	{
		scoreList[playerIndex].text = "P" + (playerIndex + 1) +  " SCORE: " + value.ToString();
	}

	public void updateLives(int playerIndex, int value)
	{
		livesList[playerIndex].text = "P" + (playerIndex + 1) +  " LIVES: " + value.ToString();
	}
}