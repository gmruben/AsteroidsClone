﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Stores information about the screen size for UI elements
/// </summary>
public class ScreenDimension
{
	private const int sizeX = 1280;
	private const int sizeY = 720;
	
	private static float _screenSize = 0;
	
	public static float screenSize
	{
		get
		{
			if (_screenSize == 0)
			{
				float ratio = (float) sizeX / sizeY;
				_screenSize = UIManager.instance.camera.aspect / ratio;
			}

			return _screenSize;
		}
	}
	
	public static float screenTop
	{
		get { return UIManager.instance.camera.orthographicSize / screenSize; }
	}
	
	public static float screenBottom
	{
		get { return -UIManager.instance.camera.orthographicSize / screenSize; }
	}
	
	public static float screenLeft
	{
		get { return -(UIManager.instance.camera.orthographicSize * UIManager.instance.camera.aspect) / screenSize; }
	}
	
	public static float screenRight
	{
		get { return (UIManager.instance.camera.orthographicSize * UIManager.instance.camera.aspect) / screenSize; }
	}

	public static Vector2 normalizedMousePosition
	{
		get
		{
			float mouseRatioX = Input.mousePosition.x / Screen.width;
			float mouseRatioY = Input.mousePosition.y / Screen.height;

			return new Vector2((mouseRatioX - 0.5f) * sizeX, (mouseRatioY - 0.5f) * sizeY);
		}
	}
}