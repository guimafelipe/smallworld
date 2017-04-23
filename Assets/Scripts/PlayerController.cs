using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private bool onFloor = false;
	private bool canJump = false;
	private bool canMove = false;
	private bool isSaying = false;
	public float hSpeed = 2f;
	private float vSpeed = 0f;

	private bool onRightWall = false, onLeftWall = false;

	public float jumpSpeed = 10f;
	public float grav = -10f;

	[SerializeField]
	private RectTransform dialogueBaloon;
	[SerializeField]
	private Text dialogueText;

	// Use this for initialization
	void Start () {
		if (dialogueBaloon == null) {
			Debug.Log ("No dialogue baloon");
		}
		if (dialogueText == null) {
			Debug.Log ("No dialogue text");
		}

		dialogueBaloon.GetComponent<Image> ().enabled = false;
		dialogueText.GetComponent<Text> ().enabled = false;

		//SayLine ("Life is good.");
	}
	
	// Update is called once per frame
	void Update () {
		doHMovment ();
		doVMovment ();
		if(Input.GetKeyDown(KeyCode.W))
			Jump(jumpSpeed);
		if (isSaying && Input.GetKeyDown (KeyCode.Space)) {
			EndLine ();
		}
	}

	//Simple horizontal moviment function
	public void doHMovment(){
		if (!canMove) 
			return;
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
		if (canJump && canMove) {
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

	public void LetMove(){ canMove = true;}

	public void DontLetMove(){ canMove = false;}

	public void SayLine(string lineToSay){
		canMove = false;
		canJump = false;
		isSaying = true;
		dialogueText.text = lineToSay;
		dialogueBaloon.GetComponent<Image> ().enabled = true;
		dialogueText.GetComponent<Text> ().enabled = true;
	}

	public void EndLine(){
		canMove = true;
		canJump = true;
		isSaying = false;
		dialogueBaloon.GetComponent<Image> ().enabled = false;
		dialogueText.GetComponent<Text> ().enabled = false;
	}
}
