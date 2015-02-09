using UnityEngine;
using System.Collections;

/// <summary>
/// This class takes care of the control of a player.
/// </summary>
public class PlayerController
{
	private readonly float acceleration;
	private readonly float maxSpeed;
	private readonly float angularSpeed;
	
	private InputController inputController;
	private PlayerAnimator playerAnimator;
	private Transform playerTransform;

	private Vector2 speed;

	private Warper warper;

	public PlayerController(InputController playerInput, PlayerAnimator playerAnimator, Transform playerTransform)
	{
		this.inputController = playerInput;
		this.playerAnimator = playerAnimator;
		this.playerTransform = playerTransform;

		//Get the parameters from game config
		acceleration = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.PlayerAcceleration);
		maxSpeed = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.PlayerMaxSpeed);
		angularSpeed = GameParamConfig.instance.retrieveParamValue<float>(GameConfigParamIds.PlayerAngularSpeed);

		speed = Vector2.zero;

		//Create Warper
		warper = new Warper(playerTransform);
	}

	public void update(float deltaTime)
	{
		float angSpeed = 0;

		if (inputController.isKey(PlayerInputKeyIds.Left)) angSpeed = -angularSpeed * deltaTime;
		else if (inputController.isKey(PlayerInputKeyIds.Right)) angSpeed = angularSpeed * deltaTime;

		Vector2 deltaSpeed = Vector2.zero;
		if (inputController.isKey (PlayerInputKeyIds.Up))
		{
			deltaSpeed = new Vector3 (playerTransform.up.x, playerTransform.up.y, 0) * acceleration * deltaTime;
		}

		speed += deltaSpeed;
		playerAnimator.thrust(deltaSpeed != Vector2.zero);

		//Clamp speed
		if (speed.sqrMagnitude > maxSpeed * maxSpeed)
		{
			speed = speed.normalized * maxSpeed;
		}

		playerTransform.position += new Vector3(speed.x, speed.y, 0) * deltaTime;
		playerTransform.Rotate(Vector3.back, angSpeed);

		warper.checkWarp();
	}

	public void reset()
	{
		speed = Vector2.zero;
	}

	public Vector2 direction
	{
		get { return speed.normalized; }
	}
}