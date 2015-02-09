using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameConfigDataList : MonoBehaviour
{
	private const float itemSize = 100;	//The vertical size of each item

	public GameObject listItem;
	public RectTransform listTransform;

	private int numItems;

	public void init(List<GameConfigData> paramIdList)
	{
		numItems = paramIdList.Count;

		//Initialise the first item
		listItem.GetComponent<ListItem>().init(paramIdList[0].id, paramIdList[0].name, GameParamConfig.instance.retrieveParamValue<string>(paramIdList[0].id));
		listItem.GetComponent<ListItem>().onChanged += onItemValueChanged;

		//Instantiate as many items as parameters
		for (int i = 1; i < numItems; i++)
		{
			GameObject item = (GameObject) GameObject.Instantiate(listItem);

			item.transform.parent = listTransform;
			item.transform.localPosition = new Vector3(listItem.transform.localPosition.x, listItem.transform.localPosition.y - (i * 100), listItem.transform.localPosition.z);

			item.GetComponent<ListItem>().init(paramIdList[i].id, paramIdList[i].name, GameParamConfig.instance.retrieveParamValue<string>(paramIdList[i].id));
			item.GetComponent<ListItem>().onChanged += onItemValueChanged;
		}

		//Set the list heigh depending on the number of items
		listTransform.sizeDelta = new Vector2 (listTransform.rect.size.x, numItems * itemSize);
	}

	/// <summary>
	/// Called when a parameter's value is changed
	/// </summary>
	/// <param name="paramId">Parameter Id.</param>
	/// <param name="value">Parameter Value.</param>
	private void onItemValueChanged(string paramId, string value)
	{
		//Store the new value for the parameter
		GameParamConfig.instance.storeValue (paramId, value);
	}
}