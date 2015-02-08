using UnityEngine;
using System.Collections;

public class WarpController
{
	private Transform cachedTransform;

	private bool hasBeenOnScreen = false;

	private readonly float left;
	private readonly float right;
	private readonly float top;
	private readonly float bottom;

	public WarpController(Transform transform)
	{
		this.cachedTransform = transform;

		//Calculate the position for the screen left, right, top and bottom and store them so we dont have to calculate them every time
		Vector3 bottomLeft = GameCamera.instance.camera.ViewportToWorldPoint(new Vector3(0.01f, 0.01f, 0.0f));
		Vector3 topRight = GameCamera.instance.camera.ViewportToWorldPoint(new Vector3(0.99f, 0.99f, 0.0f));

		left = bottomLeft.x;
		right = topRight.x;
		top = topRight.y;
		bottom = bottomLeft.y;
	}

	public void checkWarp()
	{
		Vector3 point = GameCamera.instance.camera.WorldToViewportPoint(cachedTransform.position);
		if (hasBeenOnScreen)
		{
			float posx = cachedTransform.position.x;
			float posy = cachedTransform.position.y;

			if (point.x < -0.01f) posx = right;
			else if (point.x > 1.01f) posx = left;

			if (point.y < -0.01f) posy = top;
			else if (point.y > 1.01f) posy = bottom;

			cachedTransform.position = new Vector3(posx, posy, cachedTransform.position.z);
		}
		else
		{
			if (point.x > 0.0f && point.x < 1.0f && point.y > 0.0f && point.y < 1.0f) hasBeenOnScreen = true;
		}
	}
}