using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public bool playerReached, partnerReached;

	// Use this for initialization
	void Awake () {
		playerReached = false;
		partnerReached = false;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Foot" && other.gameObject.transform.parent) {
			if (other.gameObject.transform.parent.tag == "Player")
				playerReached = true;
			else if (other.gameObject.transform.parent.tag == "Partner") {
				partnerReached = true;
			}
		}
	}
}
