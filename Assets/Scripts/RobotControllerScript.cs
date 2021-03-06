﻿using UnityEngine;
using System.Collections;

public class RobotControllerScript : MonoBehaviour {

	public float MaxSpeed = 10;
	bool facingRight = true;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;


	public float jumpForce = 200;
	float jumpTime,jumpDelay = .5f;
	bool jumped;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground",grounded);

		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D> ().velocity.y);

		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (move)); 

		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * MaxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Update(){
		if (grounded && Input.GetKeyDown(KeyCode.Space)){
			anim.SetBool("Ground", false);
			GetComponent<Rigidbody2D> ().AddForce(new Vector2(0, jumpForce ));
			jumpTime = jumpDelay;
			anim.SetTrigger ("Jump");
		}

		jumpTime = jumpTime - Time.deltaTime;
		if (jumpTime <=0 && grounded){
			anim.SetTrigger("Land");
		}

	}
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
