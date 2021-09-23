using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Esta clase controla informaciones del juego en general
 * numero de enemigos de las fases, info y textos a mostrar
 * y carga de escenas.
 */

public class GameManager : MonoBehaviour {

	public int totalTurretsAliens = 8;		// total de enemigos de clase torreta alien
	public int totalBatteries = 6;			// total de baterias a destruir antes de destrozar el cristal
	public int hardnessGlassMaster = 6;	// dureza o disparos que has de acertar para destruir el cristal
	public int currentLevel;

	public GameObject textInfo;
	public bool showTextInfo;

	public GameObject scriptRifle;

	void Start()
	{
		if (totalTurretsAliens <= 0) 
		{
			currentLevel = 1;
		}
	}

	void Update()
	{
		if (totalTurretsAliens == 8) 
		{
			StartCoroutine (Contador(" Destruye a todas las torretas aliens para entrar en el complejo !!!!"));
		} 

		if (totalBatteries == 6 && currentLevel == 1) 
		{
			StartCoroutine (Contador(" Destruye todas las baterias verdes y azules dentro del complejo !!! "));
		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.Quit ();
		}

		if (totalTurretsAliens == 0) 
		{
			StartCoroutine (Contador("Has destruido a todas las torretas!!!, Dirigete hacia la puerta con banderas"));
			totalTurretsAliens = -1;
		}

		if (totalBatteries == 0)
		{
			StartCoroutine (Contador("Has destruido todas las baterias, rompe el cristal Maestro"));
		}

		if (hardnessGlassMaster <= -1)
		{
			StartCoroutine (Contador("Lo has destruido !!!. Vuelve a casa. Mision Cumplida. "));
		}
	}

	IEnumerator Contador(string textoAMostrar)
	{
		// contador que muestra los mensajes de info un rato en pantalla al jugador
		showTextInfo = true;			
		textInfo.GetComponent<Text>().text = textoAMostrar;
		yield return new WaitForSeconds(8);
		showTextInfo = false;
	}

	void ChangeScenes()
	{
		if (currentLevel == 0) 
		{
			currentLevel = 1;
			SceneManager.LoadScene ("Level02");		// cargamos la segunda escena
			// Debug.Log("Destruye todas las baterias verdes y azules dentro del complejo");
		}
	}

	int GetTorretasAliens()
	{
		return totalTurretsAliens;		// torretas aliens vivas
	}

	void EliminateAliensTurrets()
	{
		// restamos un enemigo derrotado
		totalTurretsAliens--;

		if (totalTurretsAliens <= 0) {
			// Debug.Log ("Has destruido a todas las torretas!!!, Dirigete hacia la puerta con banderas");
			StartCoroutine (Contador("Has destruido a todas las torretas!!!, Dirigete hacia la puerta con banderas"));
		} else {
			StartCoroutine (Contador("Te quedan " + totalTurretsAliens + " torretas vivas."));
			// Debug.Log ("Te quedan " + totalTurretsAliens + " torretas vivas.");
		}
	}

	void EliminateBatteries()
	{
		totalBatteries--;

		if (totalBatteries <= 0) {
			// Debug.Log ("Has destruido todas las baterias, rompe el cristal Maestro");
			StartCoroutine (Contador("Has destruido todas las baterias, rompe el cristal Maestro"));
		} else {
			// Debug.Log ("Te quedan " + totalBatteries + " baterias activas alimentando el cristal.");
			StartCoroutine (Contador("Te quedan " + totalBatteries + " baterias activas alimentando el cristal."));
		}
	}

	void ImpactosEnCristalMaestro()
	{
		totalBatteries = -1;
		hardnessGlassMaster--;

		if (hardnessGlassMaster >= -1) 
		{
			StartCoroutine (Contador("Sigue disparando ya lo tienes !!! "));
		}
	}
}
