using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{
	private float time;
	private float intensity;

	private Transform cachedTransform;
	private Vector3 initialPosition;

	private static GameCamera _instance;

	public static GameCamera instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindGameObjectWithTag(TagNames.GameCamera).GetComponent<GameCamera>();
				_instance.init();
			}

			return _instance;
		}
	}

	/// <summary>
	/// Shakes the camera (it is used for explosions and stuff like that)
	/// </summary>
	/// <param name="time">How long the shake is.</param>
	/// <param name="intensity">How intense the shake is.</param>
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

	/// <summary>
	/// Coroutine for updating the shake.
	/// </summary>
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