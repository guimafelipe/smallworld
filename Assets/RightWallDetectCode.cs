using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWallDetectCode : MonoBehaviour {

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
			player.GetRightWall ();
		}
	}
}