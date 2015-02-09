using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class is used to spread events all over the Game, so if an especific entity needs
/// to know when an event has happened, it can subscribe to it.
/// </summary>
public class MessageBus
{
	public static event Action<bool> onGamePause;
	public static event Action onGameEnd;

	public static void dispatchGamePause(bool isPause)
	{
		if (onGamePause != null) onGamePause(isPause);
	}
	
	public static void dispatchGameEnd()
	{
		if (onGameEnd != null) onGameEnd();
	}

	/// <summary>
	/// Cleans all the listeners subscribed to the events
	/// </summary>
	public static void clean()
	{
		onGamePause = null;
		onGameEnd = null;
	}
}