using UnityEngine;
using System.Collections;

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

	public static MultiPlayerModeMenu instantiateMultiPlayerModeMenu()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Menus/MultiPlayerModeMenu");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<MultiPlayerModeMenu>();
	}
}