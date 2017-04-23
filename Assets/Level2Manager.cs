using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour {


	public Plot2 plot;
	public GameObject player;
	public GameObject fireflyPrefab;
	private GameObject firefly;
	public bool exitIntro = false;
	private bool createdFirefly1 = false, getFirefly1 = false;
	// Use this for initialization
	void Start () {
		//Stard fade in effect
		plot = GameObject.Find ("Plot2").GetComponent<Plot2> ();
		player = GameObject.Find ("Player");
		plot.currLine = 0;
		StartCoroutine (StartFirstPlot ());
	}

	IEnumerator StartFirstPlot(){
		player.GetComponent<PlayerController> ().DontLetMove ();
		yield return new WaitForSeconds (1f);
		plot.StartedPlot (0, 4);
		//plot.currLine++;
	}

	IEnumerator Spawn1Firefly(){
		player.GetComponent<PlayerController> ().canMove = false;
		yield return new WaitForSeconds (1f);
		firefly = Instantiate (fireflyPrefab, new Vector3 (3f, 0f, 0f), Quaternion.identity);
		createdFirefly1 = true;
		player.GetComponent<PlayerController> ().canMove = true;
	}

	IEnumerator RestartLevel(){
		SceneManager.LoadScene (2);
		yield return 0;
	}

	// Update is called once per frame
	void Update () {
		if (exitIntro && !createdFirefly1) {
			createdFirefly1 = true;
			StartCoroutine (Spawn1Firefly ());
		}

		if (firefly && firefly.GetComponent<Firefly> ().hasExpired) {
			DestroyImmediate (firefly);
			StartCoroutine (RestartLevel ());
		}

		if (!getFirefly1 && firefly && firefly.GetComponent<Firefly> ().gotCaught) {
			DestroyImmediate (firefly);
			getFirefly1 = true;
		}

		if (getFirefly1) {
			//StartCoroutine (SpawnPlatform1 ());
			//StartCoroutine (SpawnFirefly2 ());
		}
	}
}
