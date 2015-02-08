using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SinglePlayerGameOverMenu : UIMenu
{
	public Text highscore;
	public Text p1score;

	public UIButton retryButton;
	public UIButton mainMenuButton;

	public void init(Player player)
	{
		//highscore;
		p1score.text = "PLAYER SCORE: " + player.score;
	}

	public override void setEnabled (bool isEnabled)
	{
		
	}
}