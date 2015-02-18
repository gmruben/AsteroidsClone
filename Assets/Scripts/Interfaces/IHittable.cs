using UnityEngine;
using System.Collections;

public interface IHittable
{
	void hit(Bullet bullet, Vector3 position, Vector3 direction);
}