using UnityEngine;
using System.Collections;

public class PortionOfDoorController : MonoBehaviour {

	/* Door types are :
	 * 1 for fire door
	 * 2 for sand door
	 */
	public int doorType = 0;

	// Use this for initialization
	void Start () {
		doorType = this.transform.parent.gameObject.GetComponent<DoorController>().doorType;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy(){
		print ("Script was destroyed");

		if (this.transform.parent != null) {
			Destroy (this.transform.parent.gameObject);
		}
		else{
			Destroy(this.gameObject);
		}
	}
	
}
