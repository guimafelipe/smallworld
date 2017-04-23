using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageBehaviour : MonoBehaviour {

	public bool gotCaught = false, canDestroy = false;
	float fadeCounter = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(canDestroy){
			fadeCounter += Time.deltaTime;
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f - fadeCounter);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.transform.parent) {
			if(other.gameObject.transform.parent.tag == "Player")
				gotCaught = true;
		}
	}

	public void DestroyMirage(){
		canDestroy = true;
	}
}
