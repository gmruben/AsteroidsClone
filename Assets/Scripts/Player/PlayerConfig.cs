using UnityEngine;
using System.Collections;

public class PlayerConfig
{
	public int index;
	public PlayerInput inputController;

	public PlayerConfig(int index, PlayerInput inputController)
	{
		this.index = index;
		this.inputController = inputController;
	}
}

public class PlayerIndex
{
	public static int P1 = 0;
	public static int P2 = 1;
	public static int P3 = 2;
	public static int P4 = 3;
}