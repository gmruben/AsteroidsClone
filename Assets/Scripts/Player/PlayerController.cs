using UnityEngine;
using System.Collections;

public class PlayerController
{
	private float acceleration;
	private float maxSpeed;

	private float angularSpeed;

	private GameCamera gameCamera;
	private PlayerInput playerInput;
	private Transform playerTransform;

	private Vector2 speed;

	public PlayerController(GameCamera gameCamera, PlayerInput playerInput, Transform playerTransform)
	{
		this.gameCamera = gameCamera;
		this.playerInput = playerInput;
		this.playerTransform = playerTransform;

		//Get the parameters from game config
		acceleration = GameConfig.instance.retrieveParamValue<float>(GameConfigParamIds.PlayerAcceleration);
		maxSpeed = GameConfig.instance.retrieveParamValue<float>(GameConfigParamIds.PlayerMaxSpeed);
		angularSpeed = GameConfig.instance.retrieveParamValue<float>(GameConfigParamIds.PlayerAngularSpeed);

		speed = Vector2.zero;
	}

	public void update(float deltaTime)
	{
		float angSpeed = 0;

		if (playerInput.isKey(PlayerInputKeyIds.Left)) angSpeed = -angularSpeed * deltaTime;
		else if (playerInput.isKey(PlayerInputKeyIds.Right)) angSpeed = angularSpeed * deltaTime;

		Vector2 deltaSpeed = Vector2.zero;
		if (playerInput.isKey(PlayerInputKeyIds.Up)) deltaSpeed = new Vector3(playerTransform.up.x, playerTransform.up.y, 0) * acceleration * deltaTime;

		speed += deltaSpeed;

		//Clamp speed
		if (speed.sqrMagnitude > maxSpeed * maxSpeed) speed = speed.normalized * maxSpeed;

		playerTransform.position += new Vector3(speed.x, speed.y, 0) * deltaTime;
		playerTransform.Rotate(Vector3.back, angSpeed);

		Vector3 point = gameCamera.camera.WorldToViewportPoint(playerTransform.position);

		if (point.x < 0.0f || point.x > 1.0f) playerTransform.position = new Vector3(-playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
		if (point.y < 0.0f || point.y > 1.0f) playerTransform.position = new Vector3(playerTransform.position.x, -playerTransform.position.y, playerTransform.position.z);
	}
}