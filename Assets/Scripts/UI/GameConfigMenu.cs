using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This menu contains a list with all the game config parameters so the user can change them
/// </summary>
public class GameConfigMenu : UIMenu
{
	public GameConfigDataList itemList;

	public UIButton backButton;

	public void init()
	{
		backButton.onClick += onBackButtonClick;

		//Add here all the game config parameters that we want to be editable in the build
		List<GameConfigData> paramIdList = new List<GameConfigData> ();

		//PLAYER
		paramIdList.Add (new GameConfigData (GameConfigParamIds.PlayerAcceleration, "PLAYER ACCELERATION", ""));
		paramIdList.Add (new GameConfigData (GameConfigParamIds.PlayerMaxSpeed, "PLAYER MAX SPEED", ""));
		paramIdList.Add (new GameConfigData (GameConfigParamIds.PlayerAngularSpeed, "PLAYER ANGULAR SPEED", ""));
		paramIdList.Add (new GameConfigData (GameConfigParamIds.PlayerInvulnerableTime, "PLAYER INVULNERABLE TIME", ""));
		paramIdList.Add (new GameConfigData (GameConfigParamIds.PlayerNumLives, "PLAYER NUMBER OF LIVES", ""));

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

	public override void setEnabled (bool isEnabled)
	{

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