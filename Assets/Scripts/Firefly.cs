using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour {


	public bool gotCaught = false, hasExpired = false;
	float timeLife = 3f;

	private AudioManager audiomanager;
	// Use this for initialization
	void Start () {
		audiomanager = AudioManager.instance;
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
			if (other.gameObject.transform.parent.tag == "Player") {
				audiomanager.PlaySound ("Pickup");
				gotCaught = true;
			}
		}
	}
}
