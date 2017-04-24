using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Foot" && other.gameObject.transform.parent) {
			if (other.gameObject.transform.parent.tag == "Player")
				player.GetComponent<PlayerController>().Die();
		}
	}
}
