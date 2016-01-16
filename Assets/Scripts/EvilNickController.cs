using UnityEngine;
using System.Collections;

public class EvilNickController : MonoBehaviour {

	public Transform sightStart, sightEnd;

	public bool spotted = false;
	public bool facingLeft = true;

	public GameObject exclamPoint;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Patrol", 0f, Random.Range(2f,6f));
	}

	// Update is called once per frame
	void Update () {
		Raycasting ();
		Behaviours ();	
	}

	void Raycasting(){
		
		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);
		spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));
	}

	void Behaviours(){
		
		if (spotted == true) {
			exclamPoint.SetActive (true);
		} else {
			exclamPoint.SetActive (false);
		}

	}

	void Patrol(){

		//Changes way evilNick is looking every time the function is called	
		facingLeft = !facingLeft;

		//Flip left and right at random intervals
		if (facingLeft == true) {
			transform.eulerAngles = new Vector2 (0, 0);
		} else {
			transform.eulerAngles = new Vector2 (0,180);
		}

	}
}
