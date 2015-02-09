using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour
{
	private Animator animator;

	public SpriteRenderer ship;
	public SpriteRenderer rocket;
	public TextMesh timer;

	public void init(Color shipColor)
	{
		animator = GetComponent<Animator>();

		ship.color = shipColor;
		rocket.color = shipColor;
		timer.color = shipColor;
	}

	public void thrust(bool inThrust)
	{
		animator.SetBool("InThrust", inThrust);
	}

	public void setInvulnerable(bool isInvulnerable)
	{
		animator.SetBool("InSpawn", isInvulnerable);
	}
}