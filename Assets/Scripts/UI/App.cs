using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		//Load the save data
		GameSaveManager.loadData();

		MainMenu mainMenu = MenuManager.instantiateMainMenu ();
		mainMenu.init ();
		
		menus.Push (mainMenu);
		
	}

	public void showGameMenu()
	{
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);

		GameMenu gameMenu = MenuManager.instantiateGameMenu();
		gameMenu.init();
		
		menus.Push(gameMenu);
	}

	public void showSinglePlayerModeMenu()
	{
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);
		
		SinglePlayerModeMenu singlePlayerModeMenu = MenuManager.instantiateSinglePlayerModeMenu();
		singlePlayerModeMenu.init();
		
		menus.Push(singlePlayerModeMenu);
	}

	public void showMultiPlayerModeMenu()
	{
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);
		
		MultiPlayerModeMenu multiPlayerModeMenu = MenuManager.instantiateMultiPlayerModeMenu();
		multiPlayerModeMenu.init();
		
		menus.Push(multiPlayerModeMenu);
	}

	public void showGameConfigMenu()
	{
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);
		
		GameConfigMenu gameConfigMenuMenu = MenuManager.instantiateGameConfigMenu();
		gameConfigMenuMenu.init();
		
		menus.Push(gameConfigMenuMenu);
	}

	public void back()
	{
		UIMenu menu = menus.Pop();
		
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(true);

		GameObject.Destroy(menu.gameObject);
	}
}