using UnityEngine;
using System.Collections;

/// <summary>
/// A simple particle for the custom particle emitter
/// </summary>
public class CustomParticle : MonoBehaviour
{
	private const float lifeTime = 2.0f;
	private const float lifeSpeed = 2.5f;

	private float life;

	private float speed;
	private Vector2 direction;

	private Transform cachedTransform;
	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		//When awaking cached all the components needed and set inactive
		cachedTransform = transform;
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		gameObject.SetActive(false);
	}

	public void init(float speed, Vector2 direction, Color color)
	{
		life = lifeTime;
		gameObject.SetActive(true);

		this.speed = speed;
		this.direction = direction;

		spriteRenderer.color = color;
	}

	void Update()
	{
		life -= Time.deltaTime * lifeSpeed;

		if (life <= 0)
		{
			PoolManager.instance.destroyInstance(GetComponent<PoolInstance>());
		}
		else
		{
			cachedTransform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
			spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, life);
		}
	}
}