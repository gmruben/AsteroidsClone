using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput
{
	private Dictionary<string, KeyCode> inputMap;

	public PlayerInput(KeyCode left, KeyCode right, KeyCode up, KeyCode action)
	{
		inputMap = new Dictionary<string, KeyCode>();

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