using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameConfigMenu : UIMenu
{
	public VerticalList itemList;

	public UIButton backButton;

	public void init()
	{
		backButton.onClick += onBackButtonClick;

		List<GameConfigData> paramIdList = new List<GameConfigData> ();

		//PLAYER
		paramIdList.Add (new GameConfigData (GameConfigParamIds.PlayerAcceleration, "PLAYER ACCELERATION", ""));
		paramIdList.Add (new GameConfigData (GameConfigParamIds.PlayerMaxSpeed, "PLAYER MAX SPEED", ""));
		paramIdList.Add (new GameConfigData (GameConfigParamIds.PlayerAngularSpeed, "PLAYER ANGULAR SPEED", ""));

		//WEAPONS
		paramIdList.Add (new GameConfigData (GameConfigParamIds.GunCoolDownTime, "GUN COOLDOWN TIME", ""));
		
		//ASTEROIDS
		paramIdList.Add (new GameConfigData (GameConfigParamIds.AsteroidSmallScore, "SMALL ASTEROID SCORE", ""));
		paramIdList.Add (new GameConfigData (GameConfigParamIds.AsteroidSmallSpeed, "SMALL ASTEROID SPEED", ""));
		paramIdList.Add (new GameConfigData (GameConfigParamIds.AsteroidMediumScore, "MEDIUM ASTEROID SCORE", ""));
		paramIdList.Add (new GameConfigData (GameConfigParamIds.AsteroidMediumSpeed, "MEDIUM ASTEROID SPEED", ""));
		paramIdList.Add (new GameConfigData (GameConfigParamIds.AsteroidBigScore, "BIG ASTEROID SCORE", ""));
		paramIdList.Add (new GameConfigData (GameConfigParamIds.AsteroidBigSpeed, "BIG ASTEROID SPEED", ""));

		itemList.init (paramIdList);
	}

	private void onBackButtonClick()
	{
		App.instance.back();
	}
}

public class GameConfigData
{
	public string id;
	public string name;
	public string desc;

	public GameConfigData(string id, string name, string desc)
	{
		this.id = id;
		this.name = name;
		this.desc = desc;
	}
}