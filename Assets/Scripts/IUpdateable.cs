using UnityEngine;
using System.Collections;

/// <summary>
/// Interface for all the game objects that need to be updated from the Game
/// </summary>
public interface IUpdateable
{
	void update(float time);
}
