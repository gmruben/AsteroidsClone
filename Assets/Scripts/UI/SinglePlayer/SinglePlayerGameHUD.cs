using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This HUD is used in the single player mode
/// </summary>
public class SinglePlayerGameHUD : UIMenu
{
	public Text score;
	public Text lives;

	public UIButton pauseButton;

	public override void setEnabled (bool isEnabled)
	{
		pauseButton.setActive(isEnabled);
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