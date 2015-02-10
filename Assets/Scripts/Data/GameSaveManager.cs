using UnityEngine;
using System.Json;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class GameSaveManager
{
	private static GameSaveManager _instance;

	private GameSave _gameSave;

	public static GameSaveManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameSaveManager();
				_instance.loadData();
			}
			return _instance;
		}
	}

	public void loadData()
	{
		if (!File.Exists(Application.persistentDataPath + "/gamesave.json"))
		{
			initGameSave();
		}
		else
		{
			using(FileStream fs = new FileStream(Application.persistentDataPath + "/gamesave.json", FileMode.Open))
			{
				BinaryReader fileReader = new BinaryReader(fs);
				
				_gameSave = GameSave.parseJSonToGameSave(fileReader.ReadString());
				fs.Close();
			}
		}
	}

	public void saveData()
	{
		using(FileStream fs = new FileStream(Application.persistentDataPath + "/gamesave.json", FileMode.Create))
		{
			BinaryWriter fileWriter = new BinaryWriter(fs);
			
			fileWriter.Write(GameSave.parseGameSaveToJSon(_gameSave));
			fs.Close();
		}
	}

	private void initGameSave()
	{
		_gameSave = new GameSave();

		_gameSave.highscore = 0;
		
		saveData();
	}
	
	public GameSave gameSave
	{
		get { return _gameSave; }
		set { _gameSave = value; }
	}
}

public class GameSave
{
	public int highscore = 0;

	public static GameSave parseJSonToGameSave(string json)
	{
		GameSave gameSave = new GameSave();
		JsonObject jsonObject = JsonObject.Parse(json) as JsonObject;
		
		gameSave.highscore = int.Parse(jsonObject["highscore"].ToString());
		
		return gameSave;
	}
	
	public static string parseGameSaveToJSon(GameSave gameSave)
	{
		JsonObject jsonObject = new JsonObject();

		jsonObject.Add("highscore", gameSave.highscore);

		return jsonObject.ToString();
	}
}