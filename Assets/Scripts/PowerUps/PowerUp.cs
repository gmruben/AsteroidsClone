using UnityEngine;
using System;
using System.Collections;

public abstract class PowerUp : MonoBehaviour
{
	public event Action onPickUp;

	public GameObject sprite;
	public abstract void pickUp(Player player);
}