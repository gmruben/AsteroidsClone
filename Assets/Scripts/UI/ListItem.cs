using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ListItem : MonoBehaviour
{
	public event Action<string, string> onChanged;

	public Text title;
	public InputField input;

	private string id;

	public void init(string id, string paramTitle, string paramValue)
	{
		this.id = id;

		title.text = paramTitle;
		input.text = paramValue;
	}

	public void onInputValueChange(string value)
	{
		if (onChanged != null) onChanged (id, value);
	}
}