using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class controls all the flow in the menus.
/// </summary>
public class App : MonoBehaviour
{
	private static App _instance;
	private Stack<UIMenu> menus = new Stack<UIMenu>();

	public static App instance
	{ 
		get
		{
			return _instance;
		}
	} 
	
	void Start()
	{	
		if (_instance == null)
		{
			_instance = this;
			_instance.init();
		}
	}

	public void init()
	{
		//Create the current menu and add it to the stack
		MainMenu mainMenu = MenuManager.instantiateMainMenu ();
		mainMenu.init ();
		
		menus.Push (mainMenu);
	}

	public void showGameMenu()
	{
		//Set current menu inactive
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);

		//Create and add multiplayer mode menu
		GameMenu gameMenu = MenuManager.instantiateGameMenu();
		gameMenu.init();
		
		menus.Push(gameMenu);
	}

	public void showSinglePlayerModeMenu()
	{
		//Set current menu inactive
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);

		//Create and add multiplayer mode menu
		SinglePlayerModeMenu singlePlayerModeMenu = MenuManager.instantiateSinglePlayerModeMenu();
		singlePlayerModeMenu.init();
		
		menus.Push(singlePlayerModeMenu);
	}

	public void showMultiPlayerModeMenu()
	{
		//Set current menu inactive
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);

		//Create and add multiplayer mode menu
		MultiPlayerModeMenu multiPlayerModeMenu = MenuManager.instantiateMultiPlayerModeMenu();
		multiPlayerModeMenu.init();
		
		menus.Push(multiPlayerModeMenu);
	}

	public void showGameConfigMenu()
	{
		//Set current menu inactive
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);

		//Create and add game config menu
		GameConfigMenu gameConfigMenu = MenuManager.instantiateGameConfigMenu();
		gameConfigMenu.init();
		
		menus.Push(gameConfigMenu);
	}

	/// <summary>
	/// Goes back to the previous menu.
	/// </summary>
	public void back()
	{
		//Get last menu and set it active
		UIMenu menu = menus.Pop();
		
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(true);

		//Destroy the current menu
		GameObject.Destroy(menu.gameObject);
	}
}