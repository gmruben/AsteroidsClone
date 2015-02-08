using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
	public event Action onDead;

	public GameObject graphic;
	public PlayerTimer timer;

	private PlayerController playerController;
	private IWeaponController weaponController;
	
	[HideInInspector]
	public Transform cachedTransform;

	public PlayerAnimator playerAnimator;

	private GameModeController gameController;

	public int score { get; private set; }
	public int numLives { get; private set; }

	private bool isActive = true;

	private bool isInvulnerable = false;
	private float invulnerableTime;
	private float invulnerableCounter;

	private PlayerConfig playerConfig;

	void Update()
	{
		if (isActive)
		{
			playerController.update(Time.deltaTime);
			weaponController.update(Time.deltaTime);

			if (isInvulnerable)
			{
				invulnerableCounter -= Time.deltaTime;
				if (invulnerableCounter <= 0)
				{
					isInvulnerable = false;
					playerAnimator.setInvulnerable(false);
				}
			}
		}
	}

	public void init(GameModeController gameController, PlayerConfig playerConfig)
	{
		this.gameController = gameController;
		this.playerConfig = playerConfig;

		cachedTransform = transform;

		playerAnimator.init(playerConfig.color);

		playerController = new PlayerController(playerConfig.inputController, playerAnimator, cachedTransform);
		weaponController = new GunController(playerConfig.inputController, this);

		invulnerableTime = GameConfig.instance.retrieveParamValue<float>(GameConfigParamIds.PlayerInvulnerableTime);

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
			else if (other.CompareTag(TagNames.Asteroid) && !isInvulnerable)
			{
				other.GetComponent<Asteroid>().kill();

				CustomParticleEmitter customParticleEmitter = new CustomParticleEmitter();
				customParticleEmitter.explode2(color, cachedTransform.position);
			
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
		gameController.updateScore(this);
	}

	private void kill()
	{
		isActive = false;
		graphic.SetActive(false);

		numLives --;
		gameController.updateLives(this);

		if (numLives > 0)
		{
			StartCoroutine(respwanCoroutine());
		}
		else
		{
			if (onDead != null) onDead();
		}
	}

	private IEnumerator respwanCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		respawn();
	}

	private void respawn()
	{
		cachedTransform.position = Vector3.zero;
		cachedTransform.rotation = Quaternion.identity;
		
		playerController.reset();
		
		isActive = true;
		graphic.SetActive(true);
		
		isInvulnerable = true;
		invulnerableCounter = invulnerableTime;
		playerAnimator.setInvulnerable(true);
	}

	public void reset(int numLifes)
	{
		respawn();

		score = 0;
		this.numLives = numLifes;

		gameController.updateScore(this);
		gameController.updateLives(this);
	}

	private void onGamePause(bool isPause)
	{
		isActive = !isPause;
	}

	public int index
	{
		get { return playerConfig.index; }
	}

	public bool isDead
	{
		get { return numLives == 0; }
	}

	public Color color
	{
		get { return playerConfig.color; }
	}

	public PlayerInput inputController
	{
		get { return playerConfig.inputController; }
	}
}