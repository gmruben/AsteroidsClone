using UnityEngine;
using System;
using System.Collections;

public class MultiPlayerBox : MonoBehaviour
{
	public event Action<int, bool> onReady;

	public GameObject ready;
	public GameObject notReady;

	public bool isReady { get; private set; }
	private PlayerConfig playerConfig;

	public void init(PlayerConfig playerConfig)
	{
		this.playerConfig = playerConfig;

		ready.SetActive(false);
		notReady.SetActive(true);

		isReady  = false;
	}

	void Update()
	{
		if (playerConfig.inputController.isKeyDown(PlayerInputKeyIds.Action))
		{
			isReady = !isReady;

			ready.SetActive(isReady);
			notReady.SetActive(!isReady);

			if (onReady != null) onReady(playerConfig.index, isReady);
		}
	}
}