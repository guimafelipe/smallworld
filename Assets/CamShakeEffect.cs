using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShakeEffect : MonoBehaviour {

	public bool shakeThat = false;
	private bool updown = false, isShaking = false;
	private float epsilon = 0.5f;
	// Use this for initialization
	void Start () {
		
	}

	IEnumerator ShakeThat(){
		float cooldown = 0.2f;
		transform.position = new Vector3 (transform.position.x, transform.position.y + epsilon, transform.position.z);
		yield return new WaitForSeconds (cooldown);
		transform.position = new Vector3 (transform.position.x, transform.position.y - epsilon, transform.position.z);
		yield return new WaitForSeconds (cooldown);
		isShaking = false;
	}

	// Update is called once per frame
	void Update () {
		if (shakeThat && !isShaking) {
			isShaking = true;
			StartCoroutine (ShakeThat());
		}
	}
}
