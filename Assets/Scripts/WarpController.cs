using UnityEngine;
using System.Collections;

public class WarpController
{
	private GameCamera gameCamera;
	private Transform cachedTransform;

	private bool hasBeenOnScreen = false;

	public WarpController(GameCamera gameCamera, Transform transform)
	{
		this.gameCamera = gameCamera;
		this.cachedTransform = transform;
	}

	public void checkWarp()
	{
		Vector3 point = gameCamera.camera.WorldToViewportPoint(cachedTransform.position);
		if (hasBeenOnScreen)
		{
			if (point.x < 0.0f || point.x > 1.0f) cachedTransform.position = new Vector3(-cachedTransform.position.x, cachedTransform.position.y, cachedTransform.position.z);
			if (point.y < 0.0f || point.y > 1.0f) cachedTransform.position = new Vector3(cachedTransform.position.x, -cachedTransform.position.y, cachedTransform.position.z);
		}
		else
		{
			if (point.x > 0.0f && point.x < 1.0f && point.y > 0.0f && point.y < 1.0f) hasBeenOnScreen = true;
		}
	}
}