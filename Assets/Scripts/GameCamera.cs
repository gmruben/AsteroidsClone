using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{
	private float time;
	private float intensity;

	private Transform cachedTransform;
	private Vector3 initialPosition;

	public void shake(float time, float intensity)
	{
		this.time = time;
		this.intensity = intensity;

		StartCoroutine(updateShake());
	}

	public void init()
	{
		cachedTransform = transform;
		initialPosition = cachedTransform.position;
	}

	private IEnumerator updateShake()
	{
		while(time > 0.0f)
		{
			Vector2 randomPosition = Random.insideUnitCircle;
			cachedTransform.position = initialPosition + (new Vector3(randomPosition.x, randomPosition.y, 0) * intensity);

			time -= Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}

		cachedTransform.position = initialPosition;
	}
}