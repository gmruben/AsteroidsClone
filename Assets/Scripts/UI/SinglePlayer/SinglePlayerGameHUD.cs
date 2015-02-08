using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

	public void updateLifes(int value)
	{
		lives.text = "LIFES: " + value.ToString();
	}
}