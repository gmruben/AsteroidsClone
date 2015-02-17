using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour, IShooter, IHittable
{
	public event Action onDead;

	[HideInInspector]
	public Transform cachedTransform;

	//A GameObject with all the player graphics (so we can stop rendering it if we need to)
	public GameObject graphic;

	//A timer for the power ups
	public PlayerTimer playerTimer;
	public PlayerAnimator playerAnimator;

	//Controllers for the movement and the weapon
	private PlayerController playerController;
	private WeaponController weaponController;

	private CustomParticleEmitter customParticleEmitter;

	//A reference to the game mode controller
	private GameModeController gameController;

	public int score { get; private set; }
	public int numLives { get; private set; }

	private bool isActive = true;

	//Variables to manage invulnerability
	private bool isInvulnerable = false;
	private float invulnerableTime;
	private float invulnerableCounter;

	private PlayerConfig playerConfig;
	private PowerUp currentPowerUp;

	//The position where the player respawns when dies
	private Vector3 respawnPoint;

	void Update()
	{
		if (isActive)
		{
			playerController.update(Time.deltaTime);
			weaponController.update(Time.deltaTime);

			//Check if the invulnerability has finished
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

	void OnTriggerEnter2D(Collider2D other)
	{
		if (isActive)
		{
			//If it is a power up, pick it up
			if (other.CompareTag(TagNames.PowerUp))
			{
				//If we had a power up active, end its effect
				if (currentPowerUp != null) currentPowerUp.end();

				currentPowerUp = other.GetComponent<PowerUp>();
				currentPowerUp.pickUp(this);
			}
			//It can only be hit by an asteroid if it is not invulnerable
			else if (other.CompareTag(TagNames.Asteroid) && !isInvulnerable)
			{
				//other.GetComponent<Asteroid>().hit(this, cachedTransform.position, cachedTransform.up);
				
				GameCamera.instance.shake(0.5f,0.5f);
				customParticleEmitter.explosion(color, cachedTransform.position);
				
				hit ();
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
	}

	public void changeShootController(WeaponController controller)
	{
		weaponController = controller;
	}

	public void addScore(int score)
	{
		//The score is stored locally by each player, and then updates the game state
		this.score += score;
		gameController.updateScore(this);
	}

	public void hit()
	{
		//Set inactive until respawned
		isActive = false;
		graphic.SetActive(false);

		//When a player is hit, it loses one life
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

	public void hit(Bullet bullet, IShooter shooter, Vector3 position, Vector3 direction)
	{
		if (shooter is Player)
		{
			Player other = shooter as Player;
			if (other.index != index)
			{
				Vector2 force = (Vector2) direction * 5.0f;
				playerController.addForce(force);

				customParticleEmitter.hit(other.color, position, -direction);
				
				PoolManager.instance.destroyInstance(bullet.GetComponent<PoolInstance>());
				GameCamera.instance.shake(0.25f, 0.25f);
			}
		}
		else if (shooter is Ship)
		{
			Ship other = shooter as Ship;

			hit();
			addScore(other.score);

			customParticleEmitter.hit(Color.white, position, -direction);
			
			PoolManager.instance.destroyInstance(bullet.GetComponent<PoolInstance>());
			GameCamera.instance.shake(0.25f, 0.25f);
		}
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

	public void shoot(Vector3 direction)
	{
		Bullet bullet = EntityManager.instantiateBullet();
		
		bullet.init(color, this, direction);
		bullet.transform.position = cachedTransform.position;
	}

	public Vector2 shootDirection
	{
		get { return cachedTransform.up; }
	}
}