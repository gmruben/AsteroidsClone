using UnityEngine;
using System.Collections;

/// <summary>
/// This class controls the game camera and its different effect, like the camera shake.
/// </summary>
public class GameCamera : MonoBehaviour
{
	private float shakeTime;
	private float shakeIntensity;

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

	public void init()
	{
		cachedTransform = transform;
		initialPosition = cachedTransform.position;
	}

	/// <summary>
	/// Shakes the camera (it is used for explosions and stuff like that)
	/// </summary>
	/// <param name="time">How long the shake is.</param>
	/// <param name="intensity">How intense the shake is.</param>
	public void shake(float time, float intensity)
	{
		this.shakeTime = time;
		this.shakeIntensity = intensity;

		StartCoroutine(updateShake());
	}

	/// <summary>
	/// Coroutine for updating the shake.
	/// </summary>
	private IEnumerator updateShake()
	{
		while(shakeTime > 0.0f)
		{
			Vector2 randomPosition = Random.insideUnitCircle;
			cachedTransform.position = initialPosition + (new Vector3(randomPosition.x, randomPosition.y, 0) * shakeIntensity);

			shakeTime -= Time.deltaTime;
			yield return new WaitForSeconds(Time.deltaTime);
		}

		cachedTransform.position = initialPosition;
	}
}