using UnityEngine;
using System.Collections;

public class MenuManager
{
	public static MainMenu instantiateMainMenu()
	{
		GameObject resource = Resources.Load<GameObject>("Prefabs/Menus/MainMenu");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<MainMenu>();
	}
}