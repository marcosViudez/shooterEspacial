using UnityEngine;
using System.Collections;

/* Esta clase se usa para autodestruir cualquier objeto
 * en un tiempo determinado llamado deadTime, al cabo de 
 * ese tiempo se destruye.
 */

public class AutoDestroy : MonoBehaviour {

	public float deadTime = 0.2f;

	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, deadTime);
	}
}
