using UnityEngine;
using System;
using System.Collections;

public abstract class GameModeController
{
	public event Action onGamePause;
	public event Action onGameRestart;
	public event Action onGameEnd;

	public abstract void setActive(bool isActive);
	public abstract void updateScore(Player player);
	public abstract void updateLives(Player player);
	public abstract void reset();
	
	//Create functions for dispatching the events so they can be dispatched from the children

	protected void dispatchOnGamePause()
	{
		if (onGamePause != null) onGamePause();
	}

	protected void dispatchOnGameRestart()
	{
		if (onGameRestart != null) onGameRestart();
	}

	protected void dispatchOnGameEnd()
	{
		if (onGameEnd != null) onGameEnd();
	}
}