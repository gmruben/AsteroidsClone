using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class abstracts all the input from the user and gives access to the specific actions that a
/// player can do (left, right, up and action). When creating an instance we only need to specify
/// the keys to map to the different actions
/// </summary>
public class InputController
{
	private Dictionary<string, KeyCode> inputMap;

	public InputController(KeyCode left, KeyCode right, KeyCode up, KeyCode action)
	{
		inputMap = new Dictionary<string, KeyCode>();

		//Abstract the keyboard keys with the input controller actions
		inputMap.Add(PlayerInputKeyIds.Left, left);
		inputMap.Add(PlayerInputKeyIds.Right, right);
		inputMap.Add(PlayerInputKeyIds.Up, up);
		inputMap.Add(PlayerInputKeyIds.Action, action);
	}

	public bool isKey(string keyId)
	{
		return Input.GetKey(inputMap[keyId]);
	}

	public bool isKeyDown(string keyId)
	{
		return Input.GetKeyDown(inputMap[keyId]);
	}

	public bool isKeyUp(string keyId)
	{
		return Input.GetKeyUp(inputMap[keyId]);
	}
}

public class PlayerInputKeyIds
{
	public static string Left = "Left";
	public static string Right = "Right";
	public static string Up = "Up";
	public static string Action = "Action";
}