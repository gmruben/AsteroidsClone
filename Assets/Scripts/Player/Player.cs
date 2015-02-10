using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
	public event Action onDead;

	[HideInInspector]
	public Transform cachedTransform;

	public GameObject graphic;
	public TextMesh timer;
	public PlayerAnimator playerAnimator;

	private PlayerController playerController;
	private WeaponController weaponController;

	private CustomParticleEmitter customParticleEmitter;

	private GameModeController gameController;

	public int score { get; private set; }
	public int numLives { get; private set; }

	private bool isActive = true;

	private bool isInvulnerable = false;
	private float invulnerableTime;
	private float invulnerableCounter;

	private PlayerConfig playerConfig;
	private PowerUp currentPowerUp;

	private Vector3 respawnPoint;

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

		customParticleEmitter = new CustomParticleEmitter();

		invulnerableTime = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.PlayerInvulnerableTime);

		MessageBus.onGamePause += onGamePause;

		endTimer();
	}

	public void changeShootController(WeaponController controller)
	{
		weaponController = controller;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (isActive)
		{
			if (other.CompareTag(TagNames.PowerUp))
			{
				currentPowerUp = other.GetComponent<PowerUp>();
				currentPowerUp.pickUp(this);
			}
			else if (other.CompareTag(TagNames.Asteroid) && !isInvulnerable)
			{
				other.GetComponent<Asteroid>().hit(cachedTransform.position, cachedTransform.up);

				customParticleEmitter.explosion(color, cachedTransform.position);
				hit ();
			}
		}
	}

	public void setTimer(float time)
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

	public void addScore(int score)
	{
		//The score is stored locally by each player, and then updates the game state
		this.score += score;
		gameController.updateScore(this);
	}

	private void hit()
	{
		isActive = false;
		graphic.SetActive(false);

		numLives --;
		gameController.updateLives(this);

		//If we had a power up active, end its effect
		if (currentPowerUp != null)
		{
			currentPowerUp.end();
		}

		if (numLives > 0)
		{
			//Use a coroutine so the respawn doesn't happen immediately after dying
			StartCoroutine(respawnCoroutine());
		}
		else
		{
			//If the player has run out of lives, send the event that it is dead 
			if (onDead != null) onDead();
		}
	}

	private IEnumerator respawnCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		respawn(respawnPoint);
	}

	/// <summary>
	/// Respawns the player after dying
	/// <param name="spawnPoint">The position to be respawn at.</param>
	/// </summary>
	private void respawn(Vector3 spawnPoint)
	{
		cachedTransform.position = spawnPoint;
		cachedTransform.rotation = Quaternion.identity;
		
		playerController.reset();
		
		isActive = true;
		graphic.SetActive(true);
		
		isInvulnerable = true;
		invulnerableCounter = invulnerableTime;
		playerAnimator.setInvulnerable(true);
	}

	/// <summary>
	/// Resets a player to its initial value
	/// </summary>
	/// <param name="spawnPoint">The position to be respawn at.</param>
	/// <param name="numLives">The number of lives the player starts with.</param>
	public void reset(Vector3 spawnPoint, int numLives)
	{
		respawn(spawnPoint);

		score = 0;
		this.numLives = numLives;
		this.respawnPoint = spawnPoint;

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

	public InputController inputController
	{
		get { return playerConfig.inputController; }
	}
}