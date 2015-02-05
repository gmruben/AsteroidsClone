using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour
{
	public GameObject sprite;
	public abstract void pickUp(Player player);
}