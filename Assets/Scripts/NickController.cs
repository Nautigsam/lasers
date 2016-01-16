using UnityEngine;
using System.Collections;

public class NickController : MonoBehaviour {

	public bool isInteracting = false;
	public bool isGrounded = false;
	public Transform lineStart, lineEnd, jumpCheck;

	public float jumpForce = 100f;


	float jumpTime, jumpDelay = .5f;
	bool hasJumped;

	Animator anim;

	RaycastHit2D whatIHit;

	private float speed = 4f;
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
		} else {
			isInteracting = false;
		}

		if (Input.GetKeyDown (KeyCode.E) && isInteracting) {
			Destroy (whatIHit.collider.gameObject);
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
}
