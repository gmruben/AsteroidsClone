using UnityEngine;
using System.Collections;

/// <summary>
/// This class takes care of the player animator. The animation has 2 different layers (sprite and transform) to combine
/// them easily. All the animator logic is driven by animation parameters. 
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
	private Animator animator;

	public SpriteRenderer ship;
	public SpriteRenderer rocket;
	public TextMesh timer;

	/// <summary>
	/// Caches the animator component and sets the ship color
	/// </summary>
	/// <param name="shipColor">Ship color.</param>
	public void init(Color shipColor)
	{
		animator = GetComponent<Animator>();

		ship.color = shipColor;
		rocket.color = shipColor;
		timer.color = shipColor;
	}
	
	public void thrust(bool inThrust)
	{
		animator.SetBool(PlayerAnimationParamIds.InThrust, inThrust);
	}

	public void setInvulnerable(bool isInvulnerable)
	{
		animator.SetBool(PlayerAnimationParamIds.InSpawn, isInvulnerable);
	}
}

/// <summary>
/// Ids for the player animation parameters
/// </summary>
public class PlayerAnimationParamIds
{
	public static string InThrust = "InThrust";
	public static string InSpawn = "InSpawn";
}