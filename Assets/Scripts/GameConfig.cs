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
		parameterList.Add (GameConfigParamIds.PlayerAcceleration, "15.0");
		parameterList.Add (GameConfigParamIds.PlayerMaxSpeed, "25.0");
		parameterList.Add (GameConfigParamIds.PlayerAngularSpeed, "100.0");
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
	public static string PlayerAcceleration = "PlayerAcceleration";
	public static string PlayerAngularSpeed = "PlayerAngularSpeed";
	public static string PlayerMaxSpeed = "PlayerMaxSpeed";
}