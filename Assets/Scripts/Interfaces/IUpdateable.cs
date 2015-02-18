using UnityEngine;
using System.Collections;

/// <summary>
/// Interface for all the game objects that need to be updated from the Game
/// </summary>
public interface IUpdateable
{
	//Updates the game object
	void update(float time);
	//Disposes the game object when the game has finished
	void dispose();
}
