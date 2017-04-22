using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private bool onFloor;
	private bool canJump;
	public float hSpeed = 2f;
	private float vSpeed = 0f;

	private bool onRightWall = false, onLeftWall = false;

	public float jumpSpeed = 10f;
	public float grav = -10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		doHMovment ();
		doVMovment ();
		if(Input.GetKeyDown(KeyCode.W))
			Jump(jumpSpeed);
	}

	//Simple horizontal moviment function
	public void doHMovment(){
		float dir = Input.GetAxisRaw ("Horizontal");
		if ((dir > 0 && !onRightWall) || (dir < 0 && !onLeftWall))
			transform.position = new Vector3 (transform.position.x + hSpeed * Time.deltaTime * dir,
				transform.position.y, transform.position.z); 
	}

	//Simple vartical movment function
	public void doVMovment(){
		transform.position = new Vector3 (transform.position.x,
			transform.position.y + vSpeed * Time.deltaTime, transform.position.z);
		if(!onFloor)
			vSpeed += Time.deltaTime * grav;
	}

	public void Jump(float _jumpSpeed){
		if (canJump) {
			vSpeed = _jumpSpeed;
			onFloor = false;
			canJump = false;
		}
	}
	public void GotFloor(){
		vSpeed = 0f;
		onFloor = true;
		canJump = true;
	}

	public void ExitFloor(){ onFloor = false;}

	public void GetLeftWall(){ onLeftWall = true;}

	public void GetRightWall (){ onRightWall = true;}

	public void ExitLeftWall(){ onLeftWall = false;}

	public void ExitRightWall(){ onRightWall = false;}
}
