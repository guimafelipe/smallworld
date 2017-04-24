using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowUp : MonoBehaviour {


	private GameObject player;
	float upTH = 3f, downTH = -1f, speed = 2f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.y - transform.position.y > upTH)
			transform.position = new Vector3 (transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z); 
		if (player.transform.position.y - transform.position.y < downTH)
			transform.position = new Vector3 (transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z); 
	}
}
