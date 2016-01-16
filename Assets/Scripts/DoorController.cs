using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
