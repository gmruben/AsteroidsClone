using UnityEngine;
using System.Collections;

public class PlayerConfig
{
	public int index;
	public Color color;
	public InputController inputController;

	public PlayerConfig(int index, Color color, InputController inputController)
	{
		this.index = index;
		this.color = color;
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

public class PlayerColor
{
	public static Color P1 = new Color(250.0f / 255.0f, 50.0f / 255.0f, 50.0f / 255.0f);
	public static Color P2 = new Color(50.0f / 255.0f, 250.0f / 255.0f, 50.0f / 255.0f);
	public static Color P3 = new Color(50.0f / 255.0f, 50.0f / 255.0f, 250.0f / 255.0f);
	public static Color P4 = new Color(250.0f / 255.0f, 250.0f / 255.0f, 50.0f / 255.0f);
}