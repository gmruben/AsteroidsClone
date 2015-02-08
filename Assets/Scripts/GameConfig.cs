using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameConfig
{
	private static Dictionary<string, string> parameterList = new Dictionary<string, string>();

	private static GameConfig _instance;

	private GameConfig() {}

	public static GameConfig instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameConfig();
				_instance.init();
			}
			return _instance;
		}
	}

	public void init()
	{
		//PLAYER
		parameterList.Add (GameConfigParamIds.PlayerAcceleration, "15.0");
		parameterList.Add (GameConfigParamIds.PlayerMaxSpeed, "25.0");
		parameterList.Add (GameConfigParamIds.PlayerAngularSpeed, "150.0");
		parameterList.Add (GameConfigParamIds.PlayerInvulnerableTime, "2.5");
		parameterList.Add (GameConfigParamIds.PlayerNumLifes, "3");

		//WEAPONS
		parameterList.Add (GameConfigParamIds.GunCoolDownTime, "0.5");

		//ASTEROIDS
		parameterList.Add (GameConfigParamIds.AsteroidSmallScore, "50");
		parameterList.Add (GameConfigParamIds.AsteroidSmallSpeed, "15.0");
		parameterList.Add (GameConfigParamIds.AsteroidMediumScore, "25");
		parameterList.Add (GameConfigParamIds.AsteroidMediumSpeed, "10.0");
		parameterList.Add (GameConfigParamIds.AsteroidBigScore, "10");
		parameterList.Add (GameConfigParamIds.AsteroidBigSpeed, "5.0");
	}

	public void storeValue(string id, string value)
	{
		parameterList[id] = value;
	}

	public T retrieveParamValue<T>(string id)
	{
		object value = null;
		if (parameterList.ContainsKey(id))
		{
			string valueString = parameterList[id];
			value = retrieveValueByType<T>(valueString);
		}
		else
		{
			Debug.LogError("COULDN'T FIND PARAMETER WITH ID: " + id);
		}

		return (T)value;
	}

	private T retrieveValueByType<T>(string value)
	{
		object newValue = null;
		if (typeof(T) == typeof(string))
		{
			newValue = value;
		}
		else if (typeof(T) == typeof(int))
		{
			newValue = int.Parse(value);
		}
		else if (typeof(T) == typeof(float))
		{
			newValue = float.Parse(value);
		}
		else if (typeof(T) == typeof(bool))
		{
			newValue = (value == "1");
		}
		return (T)newValue;
	}
}

public class GameConfigParamIds
{
	//PLAYER
	public static string PlayerAcceleration = "PlayerAcceleration";
	public static string PlayerAngularSpeed = "PlayerAngularSpeed";
	public static string PlayerMaxSpeed = "PlayerMaxSpeed";
	public static string PlayerInvulnerableTime = "PlayerInvulnerableTime";
	public static string PlayerNumLifes = "PlayerNumLifes";

	//WEAPONS
	public static string GunCoolDownTime = "GunCoolDownTime";

	//ASTEROIDS
	public static string AsteroidSmallScore = "AsteroidSmallScore";
	public static string AsteroidSmallSpeed = "AsteroidSmallSpeed";
	public static string AsteroidMediumScore = "AsteroidMediumScore";
	public static string AsteroidMediumSpeed = "AsteroidMediumSpeed";
	public static string AsteroidBigScore = "AsteroidBigScore";
	public static string AsteroidBigSpeed = "AsteroidBigSpeed";
}