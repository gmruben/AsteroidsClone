using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidsGameConfig
{
	public static List<PlayerConfig> playerConfigList = new List<PlayerConfig>();
	public static string gameMode = GameModes.SinglePlayerMode;
}

public class GameModes
{
	public static string SinglePlayerMode = "SinglePlayerMode";
	public static string MultiPlayerMode = "MultiPlayerMode";
}