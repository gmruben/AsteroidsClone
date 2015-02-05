using UnityEngine;
using System.Collections;

public class PlayerTimer : MonoBehaviour
{
	private float time;
	private TextMesh timer;

	void Awake()
	{
		timer = GetComponent<TextMesh>();
		timer.gameObject.SetActive(false);
	}

	void Update()
	{
		if (time > 0)
		{
			time -= Time.deltaTime;
			timer.text = Mathf.CeilToInt(time).ToString();

			if (time <= 0) timer.gameObject.SetActive(false);
		}
	}

	public void start(float time)
	{
		this.time = time;

		timer.gameObject.SetActive(true);
		timer.text = Mathf.CeilToInt(time).ToString();
	}
}