using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

	public bool gotCaught;

	// Use this for initialization
	void Awake () {
		gotCaught = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.transform.parent) {
			if(other.gameObject.transform.parent.tag == "Player")
				gotCaught = true;
		}
	}
}
