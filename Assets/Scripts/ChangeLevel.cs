using UnityEngine;
using System.Collections;

/* Esta clase se usa para cambiar el nivel entre
 * las fases del juego al entrar en el collider 
 * usado como trigger para pasar de fase.
 */

public class ChangeLevel : MonoBehaviour {

	public GameManager gameManager;		// referencia al script gameManager
	// public int currentLevel;			

	void OnTriggerEnter(Collider other)
	{
		if (gameManager.totalTurretsAliens <= 0) 
		{
			gameManager.SendMessage ("ChangeScenes");
		}
	}
}
