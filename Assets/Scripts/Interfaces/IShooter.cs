using UnityEngine;
using System.Collections;

public interface IShooter
{
	Vector2 shootDirection { get; }

	void shoot(Vector3 direction);
}