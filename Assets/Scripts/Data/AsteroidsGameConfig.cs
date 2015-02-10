using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class stores the configuration for the game (list of players and game mode). The configuration for this game
/// is very simple, so it just has two static variables for the player list and the game mode
/// </summary>
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