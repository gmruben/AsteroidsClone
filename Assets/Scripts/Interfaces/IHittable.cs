using UnityEngine;
using System.Collections;

public interface IHittable
{
	void hit(Bullet bullet, IShooter shooter, Vector3 position, Vector3 direction);
}