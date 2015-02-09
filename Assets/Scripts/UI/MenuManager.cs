using UnityEngine;
using System.Collections;

/// <summary>
/// Class for instantiating all the different menus in the game. We could have a class with all the references to the prefabs,
/// but that would mean loading all the prefabs when loading the scene, which would increase the loading time (and it is very
/// likely that we don't need them all at onche. The bad thing about using Resoucer.Load is that we are hardcoding the path.
/// </summary>
public class MenuManager
{
	public static MainMenu instantiateMainMenu()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Menus/MainMenu");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<MainMenu>();
	}

	public static GameMenu instantiateGameMenu()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Menus/GameMenu");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<GameMenu>();
	}

	public static SinglePlayerModeMenu instantiateSinglePlayerModeMenu()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Menus/SinglePlayerModeMenu");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<SinglePlayerModeMenu>();
	}

	public static MultiPlayerModeMenu instantiateMultiPlayerModeMenu()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Menus/MultiPlayerModeMenu");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<MultiPlayerModeMenu>();
	}

	public static GameConfigMenu instantiateGameConfigMenu()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Menus/GameConfigMenu");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<GameConfigMenu>();
	}

	public static SinglePlayerGameHUD instantiateSinglePlayerGameHUD()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Menus/SinglePlayerGameHUD");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<SinglePlayerGameHUD>();
	}

	public static MultiPlayerGameHUD instantiateMultiPlayerGameHUD()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Menus/MultiPlayerGameHUD");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<MultiPlayerGameHUD>();
	}

	public static SinglePlayerGameOverMenu instantiateSinglePlayerGameOverMenu()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Menus/SinglePlayerGameOverMenu");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<SinglePlayerGameOverMenu>();
	}

	public static MultiPlayerGameOverMenu instantiateMultiPlayerGameOverMenu()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Menus/MultiPlayerGameOverMenu");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<MultiPlayerGameOverMenu>();
	}

	public static PauseMenu instantiatePauseMenu()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Menus/PauseMenu");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<PauseMenu>();
	}
}