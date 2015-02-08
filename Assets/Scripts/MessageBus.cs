using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class is used to spread events all over the Game
/// </summary>
public class MessageBus
{
	public static event Action<bool> onGamePause;

	public static void dispatchGamePause(bool isPause)
	{
		if (onGamePause != null) onGamePause(isPause);
	}
}