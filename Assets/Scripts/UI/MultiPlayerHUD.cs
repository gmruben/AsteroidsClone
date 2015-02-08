using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MultiPlayerHUD : UIMenu
{
	public Text p1Score;
	public Text p2Score;
	public Text p3Score;
	public Text p4Score;

	public void init()
	{

	}

	public void updateScore(string playerId, int value)
	{
		if (playerId == "P1") p1Score.text = "P1 SCORE: " + value.ToString();
		else if (playerId == "P2") p1Score.text = "P2 SCORE: " + value.ToString();
		else if (playerId == "P3") p1Score.text = "P3 SCORE: " + value.ToString();
		else p1Score.text = "P4 SCORE: " + value.ToString();
	}
}