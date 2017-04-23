using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = this.transform.parent.gameObject.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Colidiu");
		if (other.gameObject.tag == "World") {
			if (player.GetVSpeed () <= 0) {
				player.GotFloor ();
			}
			//player.GotFloor ();
		} else if (other.gameObject.tag == "Platform") {
			Debug.Log ("Pisou na plataforma");
			if (player.GetVSpeed () <= 0) {
				player.GotFloor ();
			}
		}
	}

	/*void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "World")
			player.GotFloor ();
	}*/

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "World" || other.gameObject.tag == "Platform") {
			player.ExitFloor ();
			Debug.Log ("Saiu do chao");
		}
	}

}
