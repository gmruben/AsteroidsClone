using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
	public event Action onDead;
	
	public PlayerTimer timer;

	private PlayerController playerController;
	private IWeaponController weaponController;
	
	[HideInInspector]
	public PlayerInput playerInput;
	[HideInInspector]
	public Transform cachedTransform;

	private Game game;
	private int score;
	private int numLifes;

	private bool isActive = true;

	void Update()
	{
		if (isActive)
		{
			playerController.update(Time.deltaTime);
			weaponController.update(Time.deltaTime);
		}
	}

	public void init(Game game)
	{
		this.game = game;

		playerInput = new PlayerInput(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow);
		cachedTransform = transform;

		playerController = new PlayerController(playerInput, cachedTransform);
		weaponController = new GunController(playerInput, this);

		score = 0;
		numLifes = 3;

		game.updateScore(0, score);
		game.updateLives(0, numLifes);

		MessageBus.onGamePause += onGamePause;
	}

	public void changeShootController(IWeaponController controller)
	{
		weaponController = controller;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (isActive)
		{
			if (other.CompareTag(TagNames.PowerUp))
			{
				other.GetComponent<PowerUp>().pickUp(this);
			}
			else if (other.CompareTag(TagNames.Asteroid))
			{
				other.GetComponent<Asteroid>().kill();

				CustomParticleEmitter customParticleEmitter = new CustomParticleEmitter();
				customParticleEmitter.explode2(cachedTransform.position);
			
				kill ();
			}
		}
	}

	public void setTimer(float time)
	{
		timer.start(time);
	}

	public void addScore(int score)
	{
		this.score += score;
		game.updateScore(0, this.score);
	}

	private void kill()
	{
		isActive = false;
		gameObject.SetActive(false);

		numLifes --;
		game.updateLives(0, numLifes);

		if (numLifes > 0)
		{
			StartCoroutine(respawn());
		}
		else
		{
			if (onDead != null) onDead();
		}
	}

	private IEnumerator respawn()
	{
		yield return new WaitForSeconds(0.5f);
		reset (Vector3.zero);
	}

	public void reset(Vector3 position)
	{
		cachedTransform.position = position;
		cachedTransform.rotation = Quaternion.identity;

		playerController.reset();

		isActive = true;
		gameObject.SetActive(true);
	}

	private void onGamePause(bool isPause)
	{
		isActive = !isPause;
	}
}