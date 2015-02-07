using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VerticalList : MonoBehaviour
{
	private const float itemSize = 75;

	public GameObject listItem;
	public RectTransform listTransform;

	private int numItems;

	public void init(List<GameConfigData> paramIdList)
	{
		numItems = paramIdList.Count;

		listItem.GetComponent<ListItem>().init(paramIdList[0].id, paramIdList[0].name, GameConfig.instance.retrieveParamValue<string>(paramIdList[0].id));
		listItem.GetComponent<ListItem>().onChanged += onItemValueChanged;

		for (int i = 1; i < numItems; i++)
		{
			GameObject item = (GameObject) GameObject.Instantiate(listItem);

			item.transform.parent = listTransform;
			item.transform.localPosition = new Vector3(listItem.transform.localPosition.x, listItem.transform.localPosition.y - (i * 100), listItem.transform.localPosition.z);

			item.GetComponent<ListItem>().init(paramIdList[i].id, paramIdList[i].name, GameConfig.instance.retrieveParamValue<string>(paramIdList[i].id));
			item.GetComponent<ListItem>().onChanged += onItemValueChanged;
		}

		listTransform.sizeDelta = new Vector2 (listTransform.rect.size.x, numItems * 100);
	}

	private void onItemValueChanged(string paramId, string value)
	{
		GameConfig.instance.storeValue (paramId, value);
	}
}