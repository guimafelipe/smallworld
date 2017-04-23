using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWallDetectCode : MonoBehaviour {

	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = this.transform.parent.gameObject.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "World") {
			player.GetLeftWall ();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "World") {
			player.ExitLeftWall ();
		}
	}
}
