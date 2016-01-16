using UnityEngine;
using System.Collections;

public class NickController : MonoBehaviour {

	private float speed = 4f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
	}

	void Movement(){
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0,0);
		}
		if (Input.GetKey (KeyCode.Q)) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0,180);
		}

	}
}
