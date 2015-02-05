using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
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

	void Update()
	{
		playerController.update(Time.deltaTime);
		weaponController.update(Time.deltaTime);
	}

	public void init(Game game)
	{
		this.game = game;

		playerInput = new PlayerInput(KeyCode.J, KeyCode.L, KeyCode.I, KeyCode.K);
		cachedTransform = transform;

		gameCamera.init();

		playerController = new PlayerController(gameCamera, playerInput, cachedTransform);
		weaponController = new GunController(gameCamera, playerInput, this);

		score = 0;
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
}