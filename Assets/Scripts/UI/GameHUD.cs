using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHUD : UIMenu
{
	public Text score;
	public Text lives;

	public void init()
	{

	}

	public void updateScore(int value)
	{
		score.text = "SCORE: " + value.ToString();
	}

	public void updateLives(int value)
	{
		lives.text = "LIVES: " + value.ToString();
	}
}