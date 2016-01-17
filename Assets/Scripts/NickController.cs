using UnityEngine;
using System.Collections;

public class NickController : MonoBehaviour {

	public GameObject inventory;

	//Booleans to describe the Player
	public bool isInteracting = false;
	public bool isGrounded = false;
	bool hasJumped;
	bool isFiringWater = false;
	bool isFiringForce = false;

	//Transforms defined around the Player
	public Transform lineStart, lineEnd, jumpCheck;

	//Different initializations of variables
	public float jumpForce = 100f;
	float jumpTime, jumpDelay = .5f;
	float firingTime, firingDelay = .2f;
	private float speed = 4f;

	Animator anim;

	RaycastHit2D whatIHit;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		RayCasting ();
	}

	void RayCasting(){

		Debug.DrawLine (lineStart.position, lineEnd.position, Color.green);
		Debug.DrawLine (this.transform.position, jumpCheck.position, Color.green);

		isGrounded = Physics2D.Linecast(this.transform.position, jumpCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		 

		if (Physics2D.Linecast (lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer ("Enemy"))) {

			//This gets a collider
			whatIHit = Physics2D.Linecast (lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer ("Enemy"));
			isInteracting = true;		
		}
		else if (Physics2D.Linecast (lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer ("ElementDoors"))) {

			//This gets a collider
			whatIHit = Physics2D.Linecast (lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer ("ElementDoors"));
			isInteracting = true;		
		} else if (Physics2D.Linecast (lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer ("ChangeScene"))) {

			//This gets a collider
			whatIHit = Physics2D.Linecast (lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer ("ChangeScene"));
			isInteracting = true;		
		}else {
			isInteracting = false;
		}

		//Pressing the left button
		if (Input.GetMouseButtonDown (0) && isInteracting) {

			//It is a door that we destroy, and it has to be of the complementary type in order to be destroyed
			if (whatIHit.collider.gameObject.layer == 11 && getLeftEquipedLaser() == whatIHit.collider.gameObject.GetComponent<PortionOfDoorController> ().doorType) {
				if (whatIHit.collider.gameObject.GetComponent<PortionOfDoorController> ().doorType == 1) {
					Debug.Log ("Je passe ici");
					isFiringWater = true;
					anim.SetBool ("firingWater", true);
					firingTime = firingDelay;
				} else if (whatIHit.collider.gameObject.GetComponent<PortionOfDoorController> ().doorType == 2) {
					isFiringForce = true;
					anim.SetBool ("firingForce", true);
					firingTime = firingDelay;
				}

				//Destroy (whatIHit.collider.gameObject);
			}
		} else if (Input.GetMouseButtonDown (1) && isInteracting) {
			
			//It is a door that we destroy, and it has to be of the complementary type in order to be destroyed
			if (whatIHit.collider.gameObject.layer == 11 && getRightEquipedLaser() == whatIHit.collider.gameObject.GetComponent<PortionOfDoorController> ().doorType) {

				if (whatIHit.collider.gameObject.GetComponent<PortionOfDoorController> ().doorType == 1) {
					isFiringWater = true;
					anim.SetBool ("firingWater", true);
					firingTime = firingDelay;
				} else if (whatIHit.collider.gameObject.GetComponent<PortionOfDoorController> ().doorType == 2) {
					isFiringForce = true;
					anim.SetBool ("firingForce", true);
					firingTime = firingDelay;
				}
				//Destroy (whatIHit.collider.gameObject);
			}
		} else if (Input.GetKey (KeyCode.F) && isInteracting) {
			if (whatIHit.collider.gameObject.layer == 12) {
				Debug.Log ("But I'm trying");
				Application.LoadLevel ("lavaScene");		
			}
		}

		if (isInteracting) { 

			firingTime -= Time.deltaTime;
			if (firingTime <= 0 ) {
				if (isFiringForce) {
					anim.SetBool ("firingForce", false);
					Destroy (whatIHit.collider.gameObject);
					isFiringForce = false;
				} else if (isFiringWater) {
					anim.SetBool ("firingWater", false);
					Destroy (whatIHit.collider.gameObject);
					isFiringWater = false;
				}
			}
		}

		Physics2D.IgnoreLayerCollision (8,9);

	}



	void Movement(){

		anim.SetFloat ("speed", Mathf.Abs (Input.GetAxis("Horizontal")));

		if (Input.GetAxisRaw ("Horizontal") > 0) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0,0);
		}
		if (Input.GetAxisRaw ("Horizontal") < 0) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0,180);
		}
		if (Input.GetKey (KeyCode.Space) && isGrounded){
			GetComponent<Rigidbody2D>().AddForce (Vector2.up * jumpForce);
			jumpTime = jumpDelay;
			anim.SetTrigger ("jump");
		}

		jumpTime -= Time.deltaTime;
		if (jumpTime <= 0 && isGrounded) {
			anim.SetTrigger ("land");
		}



	}

	int getRightEquipedLaser(){
		return LaserTypeEnumToInt (inventory.GetComponent<Inventory> ().GetRightEquippedLaser ());
	}

	int getLeftEquipedLaser(){
		return LaserTypeEnumToInt (inventory.GetComponent<Inventory> ().GetLeftEquippedLaser ());
	}

	private int LaserTypeEnumToInt (LaserType laserType) {
		switch (laserType) {
		case LaserType.None:
			return 0;
		case LaserType.Water:
			return 1;
		case LaserType.Force:
			return 2;
		}
	}

}
