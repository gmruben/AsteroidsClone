using UnityEngine;
using System.Collections;

public class CustomParticle : MonoBehaviour
{
	private float life;

	private float speed;
	private Vector2 direction;

	private Transform cachedTransform;
	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		cachedTransform = transform;
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		gameObject.SetActive(false);
	}

	public void init(float speed, Vector2 direction)
	{
		life = 2.0f;
		gameObject.SetActive(true);

		this.speed = speed;
		this.direction = direction;
	}

	void Update()
	{
		life -= Time.deltaTime * 2.5f;

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