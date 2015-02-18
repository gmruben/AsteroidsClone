using UnityEngine;
using System.Collections;

public interface IShooter
{
	Vector2 shootDirection { get; }

	void shoot(Bullet bullet, Vector3 direction);
}