using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour {


	public Plot2 plot;
	public GameObject player;
	public GameObject fireflyPrefab, platformPrefab;
	private GameObject firefly, platform1, platform2, platform3;
	private bool createdPlatform1 = false, createdPlatform2 = false, createdPlatform3 = false;
	public bool exitIntro = false;
	private bool createdFirefly1 = false, getFirefly1 = false, createdFirefly2 = false, getFirefly2 = false, createdFirefly3 = false, getFirefly3 = false, createdFirefly4 = false, getFirefly4 = false;
	private bool startedDestruction = false, plataformsDestroyed = false, levelEnded = false;

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
		//SceneManager.LoadScene (2);
		yield return 0;
	}

	IEnumerator SpawnPlatform1(){
		yield return new WaitForSeconds (0.5f);
		platform1 = Instantiate (platformPrefab, new Vector3 (-4f, -1f, 0f), Quaternion.identity);
	}

	IEnumerator SpawnFirefly2(){
		player.GetComponent<PlayerController> ().canMove = false;
		yield return new WaitForSeconds(1f);
		firefly = Instantiate (fireflyPrefab, new Vector3 (-4f, 1f, 0f), Quaternion.identity);
		createdFirefly2 = true;
		player.GetComponent<PlayerController> ().canMove = true;
	}

	IEnumerator SpawnPlatform2(){
		yield return new WaitForSeconds (0.5f);
		platform2 = Instantiate (platformPrefab, new Vector3 (0f, 0f, 0f), Quaternion.identity);
	}

	IEnumerator SpawnFirefly3(){
		player.GetComponent<PlayerController> ().canMove = false;
		yield return new WaitForSeconds(1f);
		firefly = Instantiate (fireflyPrefab, new Vector3 (0f, 2f, 0f), Quaternion.identity);
		createdFirefly3 = true;
		player.GetComponent<PlayerController> ().canMove = true;
	}

	IEnumerator SpawnPlatform3(){
		yield return new WaitForSeconds (0.5f);
		platform3 = Instantiate (platformPrefab, new Vector3 (4f, 1f, 0f), Quaternion.identity);
	}

	IEnumerator SpawnFirefly4(){
		player.GetComponent<PlayerController> ().canMove = false;
		yield return new WaitForSeconds(1f);
		firefly = Instantiate (fireflyPrefab, new Vector3 (4f, 2.5f, 0f), Quaternion.identity);
		createdFirefly3 = true;
		player.GetComponent<PlayerController> ().canMove = true;
	}

	IEnumerator DestroyPlatforms(){

		player.GetComponent<PlayerController> ().canMove = false;
		yield return new WaitForSeconds (0.5f);
		Destroy (platform1);
		yield return new WaitForSeconds (0.5f);
		Destroy (platform2);
		yield return new WaitForSeconds (0.5f);
		Destroy (platform3);
		player.GetComponent<PlayerController> ().ExitFloor();
		plataformsDestroyed = true;
	}

	IEnumerator EndLevel(){
		player.GetComponent<PlayerController> ().DontLetMove ();
		yield return new WaitForSeconds (1f);
		plot.StartedPlot (4, 8);
	}

	// Update is called once per frame
	void Update () {
		if (exitIntro && !createdFirefly1) {
			createdFirefly1 = true;
			StartCoroutine (Spawn1Firefly ());
		}

		if (plataformsDestroyed && player.GetComponent<PlayerController>().IsOnFloor() && !levelEnded) {
			levelEnded = true;
			StartCoroutine (EndLevel());
		}

		if (getFirefly4 && !startedDestruction) {
			startedDestruction = true;
			StartCoroutine (DestroyPlatforms ());
		}

		if (createdFirefly4 && !getFirefly4 && firefly && firefly.GetComponent<Firefly> ().gotCaught) {
			DestroyImmediate (firefly);
			getFirefly4 = true;
		}

		if (getFirefly3 && !createdFirefly4 && !createdPlatform3) {
			createdPlatform3 = true;
			createdFirefly4 = true;
			StartCoroutine (SpawnPlatform3 ());
			StartCoroutine (SpawnFirefly4 ());
		}

		if (createdFirefly3 && !getFirefly3 && firefly && firefly.GetComponent<Firefly> ().gotCaught) {
			DestroyImmediate (firefly);
			getFirefly3 = true;
		}

		if (getFirefly2 && !createdFirefly3 && !createdPlatform2) {
			createdPlatform2 = true;
			createdFirefly3 = true;
			StartCoroutine (SpawnPlatform2 ());
			StartCoroutine (SpawnFirefly3 ());
		}

		if (createdFirefly2 && !getFirefly2 && firefly && firefly.GetComponent<Firefly> ().gotCaught) {
			DestroyImmediate (firefly);
			getFirefly2 = true;
		}

		if (getFirefly1 && !createdFirefly2 && !createdPlatform1) {
			createdPlatform1 = true;
			createdFirefly2 = true;
			StartCoroutine (SpawnPlatform1 ());
			StartCoroutine (SpawnFirefly2 ());
		}

		if (firefly && firefly.GetComponent<Firefly> ().hasExpired) {
			DestroyImmediate (firefly);
			StartCoroutine (RestartLevel ());
		}

		if (createdFirefly1 && !getFirefly1 && firefly && firefly.GetComponent<Firefly> ().gotCaught) {
			DestroyImmediate (firefly);
			getFirefly1 = true;
		}
	
	}
}
