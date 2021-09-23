using UnityEngine;
using System.Collections;

/* Esta clase controla al jugador, los raycast del arma
 * las particulas creadas al impactar en objetos, todos los 
 * gameobjects del juego. Impactos y creacion de agujeros
 * en gameObjects.Y control de la mirilla del arma.
 */

public class RifleController : MonoBehaviour {


	RaycastHit inforayo;						// info del raycast
	RaycastHit inforayoCrossFire;				// info mirilla 

	public GameManager gameManager;				// referencia al script gameManager

	public Transform shootInitPosition;			// posicion de la creacion de las balas y particulas
	public GameObject shootParticles;			// particulas de fuego del rifle

	public GameObject crossFireWeapon;			// objeto para mostrar la mirilla del rifle
	private GameObject mirilla;

	public GameObject alienTurretsParticles;	// explosion de las torretas y las baterias verdes
	public GameObject blueBatteryParticles;		// explosion de las baterias azules
	public GameObject masterGlassParticles;	// impactos de particula en el cristal grande
	public GameObject cristalParticles;			// explosion del cristal fase final

	public GameObject holeShoot;				// gameObject de los agujeros que hacen las balas al colisionar
		
	public float distWeapon = 50.0f;			// distancia o alcance del arma al disparar

	// Use this for initialization
	void Start () 
	{
		// Debug.Log (" Destruye a todas las torretas aliens para entrar en el complejo!!!!");
		mirilla = Instantiate (crossFireWeapon, Vector3.zero, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//textos.SetActive (activarTexto);		// activamos el texto de info de juego


		// si pulsamos el boton izquierdo del raton disparamos un rayo
		if (Input.GetMouseButtonDown (0)) 
		{
			ParticlesCreation ();		// llamamos al metodo para crear las particulas en el arma al disparar
			HoleCreateAndProp();		// llamamos al metodo para crear el agujero y propiedades
		}
	}

	void ParticlesCreation()
	{
		// instanciamos las particulas en el punta del rifle
		GameObject auxShootParticles = Instantiate (shootParticles, shootInitPosition.transform.position, shootInitPosition.rotation) as GameObject;
		// la hacemos hija del gameobject rifle para mantener la posicion
		auxShootParticles.transform.parent = gameObject.transform;
	}

	void HoleCreateAndProp()
	{
		if (Physics.Raycast (shootInitPosition.transform.position, shootInitPosition.transform.forward, out inforayo, distWeapon)) 
		{
			// impacto en enemigos
			DestroyEnemiesProp();

			// instanciamos el agujero que crea la bala al colisionar
			GameObject auxHoleShoot = Instantiate (holeShoot, inforayo.point, Quaternion.identity) as GameObject;
			auxHoleShoot.transform.forward = -inforayo.normal;			// colocamos al objeto 
			auxHoleShoot.transform.Translate (0, 0, -0.001f);			// lo trasladamos un poco en eje z para que no se solape con el objeto que colisiona
			auxHoleShoot.transform.parent = inforayo.transform;		    // lo hacemos hijo del gameobject que colisiona
		}
		// dibujamos el raycast para ver donde esta
		// Debug.DrawRay (shootInitPosition.position, shootInitPosition.transform.forward * 80f,Color.red, 10f);
	}

	void DestroyEnemiesProp()
	{
		if (inforayo.transform.gameObject.tag == "alienTurrets") 
		{
			// destruimos la torreta enemiga
			Destroy (inforayo.transform.gameObject);	
			// instanciamos la explosion de particulas de la torreta alien
			Instantiate (alienTurretsParticles, inforayo.transform.position + new Vector3(0,3,0), inforayo.transform.rotation);
			// llamamos al metodo que nos resta torretas del gameManager
			gameManager.SendMessage ("EliminateAliensTurrets");
		}

		if (inforayo.transform.gameObject.tag == "blueBattery") 
		{
			// destruimos la torreta enemiga
			Destroy (inforayo.transform.gameObject);	
			// instanciamos la explosion de particulas de la torreta alien
			Instantiate (blueBatteryParticles, inforayo.transform.position + new Vector3(0,0,0), inforayo.transform.rotation);
			// llamamos al metodo que nos resta torretas del gameManager
			gameManager.SendMessage ("EliminateBatteries");
		}

		if (inforayo.transform.gameObject.tag == "greenBattery") 
		{
			// destruimos la torreta enemiga
			Destroy (inforayo.transform.gameObject);	
			// instanciamos la explosion de particulas de la torreta alien
			Instantiate (alienTurretsParticles, inforayo.transform.position, inforayo.transform.rotation);
			// llamamos al metodo que nos resta torretas del gameManager
			gameManager.SendMessage ("EliminateBatteries");
		}

		if (inforayo.transform.gameObject.tag == "masterCristal") 
		{
			// destruimos la torreta enemiga
			if (gameManager.hardnessGlassMaster == 0)
			{
				Destroy (inforayo.transform.gameObject);
				Instantiate (cristalParticles, inforayo.transform.position + new Vector3(0,0,0), inforayo.transform.rotation);
			}

			// instanciamos la explosion de particulas de la torreta alien
			Instantiate (masterGlassParticles, inforayo.point, Quaternion.identity);
			// llamamos al metodo que nos resta torretas del gameManager
			if(gameManager.totalBatteries <= 0)
			{
				gameManager.SendMessage ("ImpactosEnCristalMaestro");
			}
		}
	}

	void LateUpdate()
	{
		if (Physics.Raycast (shootInitPosition.transform.position, shootInitPosition.transform.forward, out inforayoCrossFire, distWeapon))
		{
			mirilla.transform.forward = -inforayoCrossFire.normal;
			mirilla.transform.position = inforayoCrossFire.point;
		}
		// Debug.DrawRay (shootInitPosition.position, shootInitPosition.transform.forward * distWeapon,Color.green, 10f);
	}
}
