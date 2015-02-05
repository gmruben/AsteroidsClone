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
		MainMenu mainMenu = MenuManager.instantiateMainMenu ();
		mainMenu.init ();
		
		menus.Push (mainMenu);
		
	}

	/*public void showArcadeModeMenu()
	{
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(false);

		ArcadeModeMenu arcadeModeMenu = MenuManager.instantiateArcadeModeMenu();
		arcadeModeMenu.init();
		
		menus.Push(arcadeModeMenu);
	}*/

	public void back()
	{
		UIMenu menu = menus.Pop();
		
		UIMenu currentMenu = menus.Peek();
		currentMenu.setActive(true);

		GameObject.Destroy(menu.gameObject);
	}
}