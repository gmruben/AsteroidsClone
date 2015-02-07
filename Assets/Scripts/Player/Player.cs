using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
	public event Action onDead;

	public GameCamera gameCamera;
	public PlayerTimer timer;

	private PlayerController playerController;
	private IWeaponController weaponController;

	[HideInInspector]
	public PlayerInput playerInput;
	[HideInInspector]
	public Transform cachedTransform;

	private Game game;
	private int score;

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

		gameCamera.init();

		playerController = new PlayerController(gameCamera, playerInput, cachedTransform);
		weaponController = new GunController(gameCamera, playerInput, this);

		score = 0;

		MessageBus.onGamePause += onGamePause;
	}

	public void changeShootController(IWeaponController controller)
	{
		weaponController = controller;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(TagNames.PowerUp))
		{
			other.GetComponent<PowerUp>().pickUp(this);
		}
		else if (other.CompareTag(TagNames.Asteroid))
		{
			other.GetComponent<Asteroid>().kill();

			CustomParticleEmitter customParticleEmitter = new CustomParticleEmitter();
			
			customParticleEmitter.init();
			customParticleEmitter.explode2(cachedTransform.position);

			if (onDead != null) onDead();
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

	private void onGamePause(bool isPause)
	{
		isActive = !isPause;
	}
}