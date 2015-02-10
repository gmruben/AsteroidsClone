using UnityEngine;
using System.Collections;

/// <summary>
/// This class controls the litle time the player has to show how much longer can use a power up
/// </summary>
public class PlayerTimer : MonoBehaviour
{
	private TextMesh timer;

	void Awake()
	{
		timer = GetComponent<TextMesh>();
		endTimer();
	}

	public void startTimer(float time)
	{
		timer.gameObject.SetActive(true);
		updateTimer(time);
	}
	
	public void updateTimer(float time)
	{
		timer.text = Mathf.CeilToInt(time).ToString();
	}
	
	public void endTimer()
	{
		timer.gameObject.SetActive(false);
	}
}
