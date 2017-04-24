using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerBehaviour : MonoBehaviour {

	public bool onFloor = false;
	public bool canJump = false;
	public bool canMove = false;
	private bool isSaying = false;
	public float hSpeed = 2f;
	private float vSpeed = 0f;


	private Animator animator;
	[SerializeField]
	private GameObject graphics;

	private bool onRightWall = false, onLeftWall = false;

	public float vVmax = 10f;
	public float jumpSpeed = 10f;
	public float grav = -10f;
	private float movDir = 0;
	public bool isDead;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		doVMovment ();
		doHMovment (movDir);
	}

	public void doHMovment(float dir){
		transform.position = new Vector3 (transform.position.x + hSpeed * Time.deltaTime * dir,
			transform.position.y, transform.position.z); 
		if (dir < 0) {
			animator.SetBool ("isWalking", true);
			graphics.GetComponent<SpriteRenderer> ().flipX = true;
		} else if (dir > 0) {
			animator.SetBool ("isWalking", true);
			graphics.GetComponent<SpriteRenderer> ().flipX = false;
		} else {
			animator.SetBool ("isWalking", false);
		}
			
	}

	public void doVMovment(){
		if (onFloor)
			vSpeed = 0;
		transform.position = new Vector3 (transform.position.x,
			transform.position.y + vSpeed * Time.deltaTime, transform.position.z);
		if(!onFloor && Mathf.Abs(vSpeed) < vVmax)
			vSpeed += Time.deltaTime * grav;

	}


	public void Jump(float _jumpSpeed){
		vSpeed = _jumpSpeed;
		onFloor = false;
		animator.SetBool ("isJumping", true);
		Debug.Log ("pulei");
	}

	public void GotFloor(){
		animator.SetBool ("isJumping", false);
		vSpeed = 0f;
		onFloor = true;
		canJump = true;
	}

	public void ExitFloor(){ onFloor = false;}

	public void GetLeftWall(){ onLeftWall = true;}

	public void GetRightWall (){ onRightWall = true;}

	public void ExitLeftWall(){ onLeftWall = false;}

	public void ExitRightWall(){ onRightWall = false;}

	public void LetMove(){ canMove = true;}

	public void DontLetMove(){ canMove = false;}

	public float GetVSpeed(){ return vSpeed;}

	public void SetMovDir(float _dir){
		movDir = _dir;
	} 

	public bool IsOnFloor(){ return onFloor; }

	public void Die(){
		isDead = true;
	}
}
