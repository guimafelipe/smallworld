using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour {


	public bool gotCaught = false, hasExpired = false;
	float timeLife = 3f;

	// Use this for initialization
	void Start () {
		timeLife = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		timeLife -=Time.deltaTime;
		if (timeLife < 0) {
			hasExpired = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.transform.parent) {
			if(other.gameObject.transform.parent.tag == "Player")
				gotCaught = true;
		}
	}
}
