﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public bool onFloor = false;
	public bool canJump = false;
	public bool canMove = false;
	private bool isSaying = false;
	public float hSpeed = 2f;
	private float vSpeed = 0f;

	private AudioManager audioManager;
	private Animator animator;
	[SerializeField]
	private GameObject graphics;

	private bool onRightWall = false, onLeftWall = false;

	public float vVmax = 10f;
	public float jumpSpeed = 10f;
	public float grav = -10f;
	public bool isDead;

	[SerializeField]
	private RectTransform dialogueBaloon;
	[SerializeField]
	private Text dialogueText;

	// Use this for initialization
	void Start () {
		isDead = false;
		onFloor = false;
		audioManager = AudioManager.instance;
		if (dialogueBaloon == null) {
			Debug.Log ("No dialogue baloon");
		}
		if (dialogueText == null) {
			Debug.Log ("No dialogue text");
		}

		animator = this.GetComponent<Animator> ();
		dialogueBaloon.GetComponent<Image> ().enabled = false;
		dialogueText.GetComponent<Text> ().enabled = false;

		//SayLine ("Life is good.");
	}
	
	// Update is called once per frame
	void Update () {
		doHMovment ();
		doVMovment ();
		if(Input.GetKeyDown(KeyCode.W)){
			//Debug.Log ("Leu que pulou");
			Jump(jumpSpeed);
		}

		if (transform.position.y < -6f)
			Die ();
	}

	//Simple horizontal moviment function
	public void doHMovment(){
		if (!canMove) {
			animator.SetBool ("isWalking", false);
			return;
		}
		float dir = Input.GetAxisRaw ("Horizontal");
		if ((dir > 0 && !onRightWall) || (dir < 0 && !onLeftWall)) {
			transform.position = new Vector3 (transform.position.x + hSpeed * Time.deltaTime * dir,
				transform.position.y, transform.position.z); 
			animator.SetBool ("isWalking", true);
			if (dir < 0) {
				graphics.GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				graphics.GetComponent<SpriteRenderer> ().flipX = false;
			}
		} else {
			animator.SetBool ("isWalking", false);
		}
	}

	//Simple vartical movment function
	public void doVMovment(){
		if (onFloor)
			vSpeed = 0;
		transform.position = new Vector3 (transform.position.x,
			transform.position.y + vSpeed * Time.deltaTime, transform.position.z);
		if(!onFloor && Mathf.Abs(vSpeed) < vVmax)
			vSpeed += Time.deltaTime * grav;
		
	}

	public void Jump(float _jumpSpeed){
		if (canJump && canMove) {
			audioManager.PlaySound ("Jump");
			vSpeed = _jumpSpeed;
			onFloor = false;
			canJump = false;
			animator.SetBool ("isJumping", true);
		}
	}

	public void GotFloor(){
		//Debug.Log ("Chamou got floor");
		animator.SetBool("isJumping", false);
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

	public bool IsOnFloor(){ return onFloor; }

	public void Die(){
		if (!isDead) {
			audioManager.PlaySound ("Morte");
			DontLetMove ();
			isDead = true;
		}
	}


	public void SayLine(string lineToSay){
		Debug.Log ("Chamou player say line");
		canMove = false;
		canJump = false;
		isSaying = true;
		dialogueText.text = lineToSay;
		dialogueBaloon.GetComponent<Image> ().enabled = true;
		dialogueText.GetComponent<Text> ().enabled = true;
	}

	public void EndLine(){
		Debug.Log ("Chamou player end line");
		isSaying = false;
		dialogueBaloon.GetComponent<Image> ().enabled = false;
		dialogueText.GetComponent<Text> ().enabled = false;
	}
}
