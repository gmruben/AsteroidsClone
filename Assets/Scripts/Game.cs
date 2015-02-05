using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public GameHUD gameHUD;
	public Player player;

	void Awake()
	{
		init ();
	}

	public void init()
	{
		player.init(this);
		gameHUD.init();
	}

	public void updateScore(int playerIndex, int score)
	{
		gameHUD.updateScore(score);
	}
}