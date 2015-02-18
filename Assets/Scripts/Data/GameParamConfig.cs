using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This classs stores all the different parameters used in the game.
/// </summary>
public class GameParamConfig
{
	private static Dictionary<string, string> parameterList = new Dictionary<string, string>();

	private static GameParamConfig _instance;

	private GameParamConfig() {}

	public static GameParamConfig instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameParamConfig();
				_instance.init();
			}
			return _instance;
		}
	}

	public void init()
	{
		//Here we initialise the values for all the different parameters in the game

		//PLAYER
		parameterList.Add (GameConfigParamIds.PlayerAcceleration, "15.0");
		parameterList.Add (GameConfigParamIds.PlayerMaxSpeed, "25.0");
		parameterList.Add (GameConfigParamIds.PlayerAngularSpeed, "150.0");
		parameterList.Add (GameConfigParamIds.PlayerInvulnerableTime, "2.5");
		parameterList.Add (GameConfigParamIds.PlayerNumLives, "3");

		//WEAPONS
		parameterList.Add (GameConfigParamIds.GunCoolDownTime, "0.5");
		parameterList.Add (GameConfigParamIds.MachineGunCoolDownTime, "0.15");
		parameterList.Add (GameConfigParamIds.HeavyMachineGunCoolDownTime, "0.10");
		parameterList.Add (GameConfigParamIds.ShotGunCoolDownTime, "1.25");

		//POWER UPS
		parameterList.Add (GameConfigParamIds.PowerUpSpawnTime, "5");
		parameterList.Add (GameConfigParamIds.MachineGunPowerUpTime, "10.0");
		parameterList.Add (GameConfigParamIds.HeavyMachineGunPowerUpTime, "5.0");
		parameterList.Add (GameConfigParamIds.ShotGunPowerUpTime, "5.0");

		//ASTEROIDS
		parameterList.Add (GameConfigParamIds.AsteroidSpawnTime, "5");
		parameterList.Add (GameConfigParamIds.AsteroidSmallScore, "50");
		parameterList.Add (GameConfigParamIds.AsteroidSmallSpeed, "15.0");
		parameterList.Add (GameConfigParamIds.AsteroidMediumScore, "25");
		parameterList.Add (GameConfigParamIds.AsteroidMediumSpeed, "10.0");
		parameterList.Add (GameConfigParamIds.AsteroidBigScore, "10");
		parameterList.Add (GameConfigParamIds.AsteroidBigSpeed, "5.0");

		//SHIP
		parameterList.Add (GameConfigParamIds.ShipSpawnTime, "15");
		parameterList.Add (GameConfigParamIds.ShipScore, "100");
		parameterList.Add (GameConfigParamIds.ShipSpeed, "10.0");
	}

	public void storeValue(string id, string value)
	{
		parameterList[id] = value;
	}

	/// <summary>
	/// Retrieves a parameters value by its id
	/// </summary>
	/// <returns>The parameter value.</returns>
	/// <param name="id">The parameter id.</param>
	/// <typeparam name="T">The type of the value we want to retrieve.</typeparam>
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

	/// <summary>
	/// Casts a value in string format to an specific type
	/// </summary>
	/// <returns>The value casted to the correct type.</returns>
	/// <param name="value">Value.</param>
	/// <typeparam name="T">The type of the value we want to cast it to.</typeparam>
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

/// <summary>
/// This class contains all the Ids for all the game config parameters.
/// </summary>
public class GameConfigParamIds
{
	//PLAYER
	public static string PlayerAcceleration = "PlayerAcceleration";
	public static string PlayerAngularSpeed = "PlayerAngularSpeed";
	public static string PlayerMaxSpeed = "PlayerMaxSpeed";
	public static string PlayerInvulnerableTime = "PlayerInvulnerableTime";
	public static string PlayerNumLives = "PlayerNumLives";

	//WEAPONS
	public static string GunCoolDownTime = "GunCoolDownTime";
	public static string MachineGunCoolDownTime = "MachineGunCoolDownTime";
	public static string HeavyMachineGunCoolDownTime = "HeavyMachineGunCoolDownTime";
	public static string ShotGunCoolDownTime = "ShotGunCoolDownTime";

	//POWER UPS
	public static string PowerUpSpawnTime = "PowerUpSpawnTime";
	public static string MachineGunPowerUpTime = "MachineGunPowerUpTime";
	public static string HeavyMachineGunPowerUpTime = "HeavyMachineGunPowerUpTime";
	public static string ShotGunPowerUpTime = "ShotGunPowerUpTime";

	//ASTEROIDS
	public static string AsteroidSpawnTime = "AsteroidSpawnTime";
	public static string AsteroidSmallScore = "AsteroidSmallScore";
	public static string AsteroidSmallSpeed = "AsteroidSmallSpeed";
	public static string AsteroidMediumScore = "AsteroidMediumScore";
	public static string AsteroidMediumSpeed = "AsteroidMediumSpeed";
	public static string AsteroidBigScore = "AsteroidBigScore";
	public static string AsteroidBigSpeed = "AsteroidBigSpeed";

	//SHIPS
	public static string ShipSpawnTime = "ShipSpawnTime";
	public static string ShipScore = "ShipScore";
	public static string ShipSpeed = "ShipSpeed";
}