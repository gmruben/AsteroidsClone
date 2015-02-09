using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class for all the Menu Classes
/// </summary>
public abstract class UIMenu : MonoBehaviour
{
	void Start()
	{
		GetComponent<Canvas>().worldCamera = UICamera.instance.cachedCamera;
		GetComponent<RectTransform>().localScale = Vector3.one * ScreenDimension.screenSize;

		Transform topTransform = transform.FindChild("Top");
		Transform topRightTransform = transform.FindChild("TopRight");
		Transform leftTransform = transform.FindChild("Left");
		Transform bottomTransform = transform.FindChild("Bottom");

		if (topTransform != null) topTransform.localPosition = new Vector3(topTransform.localPosition.x, ScreenDimension.screenTop, topTransform.localPosition.z);
		if (topRightTransform != null) topRightTransform.localPosition = new Vector3(ScreenDimension.screenRight, ScreenDimension.screenTop, topRightTransform.localPosition.z);
		if (leftTransform != null) leftTransform.localPosition = new Vector3(ScreenDimension.screenLeft, leftTransform.localPosition.y, leftTransform.localPosition.z);
		if (bottomTransform != null) bottomTransform.localPosition = new Vector3(bottomTransform.localPosition.x, ScreenDimension.screenBottom, bottomTransform.localPosition.z);
	}

	public virtual void setActive(bool isActive)
	{
		gameObject.SetActive(isActive);
	}
	
	public abstract void setEnabled(bool isEnabled);
}