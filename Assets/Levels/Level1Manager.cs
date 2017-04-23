using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour {

	public Plot1 plot;
	public GameObject player;
	public GameObject flowerPrefab, miragePrefab;
	public bool exitIntro = false, canEnd = false; 
	public bool gotFlower1 = false, createdFlower1 = false;
	public bool gotFlower2 = false, createdFlower2 = false;
	public bool createdMirage = false, gotMirage = false;
	private GameObject flower, spike, mirage;
	// Use this for initialization
	void Start () {
		//Stard fade in effect
		plot = GameObject.Find("Plot1").GetComponent<Plot1>();
		player = GameObject.Find("Player");
		plot.currLine = 0;
		StartCoroutine (StartFirstPlot ());
		//plot.StartedPlot ();


	}

	IEnumerator StartFirstPlot(){
		player.GetComponent<PlayerController> ().DontLetMove ();
		yield return new WaitForSeconds (1f);
		plot.StartedPlot (0, 10);
		//plot.currLine++;
	}

	IEnumerator SpawnFirstFlower(){
		player.GetComponent<PlayerController> ().DontLetMove ();
		yield return new WaitForSeconds (1.5f);
		flower = Instantiate (flowerPrefab, new Vector3 (4.7f, -1f, 0f), Quaternion.identity);
		createdFlower1 = true;
		plot.StartedPlot (10, 14);
	}

	IEnumerator SpawnSecondFlower(){
		player.GetComponent<PlayerController> ().DontLetMove ();
		yield return new WaitForSeconds (1.5f);
		flower = Instantiate (flowerPrefab, new Vector3 (-4.7f, -1f, 0f), Quaternion.identity);
		createdFlower2 = true;
		plot.StartedPlot (14, 16);
	}

	IEnumerator RestartLevel(){
		//FadeOutAnimation ();
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(1);
	}

	IEnumerator SpawnMirage(){
		player.GetComponent<PlayerController> ().DontLetMove ();
		yield return new WaitForSeconds (1.5f);
		createdMirage = true;
		plot.StartedPlot (16, 20);
		mirage = Instantiate (miragePrefab, new Vector3 (1f, 1f, 0f), Quaternion.identity);
	}

	IEnumerator DestroyMirage(){
		mirage.GetComponent<MirageBehaviour> ().DestroyMirage();
		player.GetComponent<PlayerController> ().DontLetMove ();
		yield return new WaitForSeconds (1);
		plot.StartedPlot (20, 21);
	}

	IEnumerator EndLevel(){
		//FadeOutAnim
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(2);
		yield return 0;
	}

	// Update is called once per frame
	void Update () {

		if (player.GetComponent<PlayerController> ().isDead) {
			StartCoroutine (RestartLevel ());
		}

		if (player.GetComponent<PlayerController> ().isDead) {
			StartCoroutine (RestartLevel ());
		}

		//Spike spike;
		if (exitIntro && !createdFlower1) {
			StartCoroutine (SpawnFirstFlower());
			createdFlower1 = true;
		}
		if (flower != null && flower.GetComponent<Flower>().gotCaught && createdFlower1 && !gotFlower1) {
			DestroyImmediate (flower);
			gotFlower1 = true;
		}
		if (gotFlower1 && !createdFlower2) {
			Debug.Log ("Spanw segunda");
			StartCoroutine(SpawnSecondFlower ());
			createdFlower2 = true;
		}
		if (flower != null && flower.GetComponent<Flower> ().gotCaught && createdFlower2 && !gotFlower2) {
			Debug.Log ("Destruiu segunda flor");
			DestroyImmediate (flower);
			gotFlower2 = true;
		}
		if (gotFlower2 && !createdMirage) {
			Debug.Log ("Criou miragem");
			StartCoroutine (SpawnMirage ());
			createdMirage = true;
		}

		if (mirage != null && mirage.GetComponent<MirageBehaviour> ().gotCaught && !gotMirage) {
			gotMirage = true;
			StartCoroutine(DestroyMirage ());
		}
		if (canEnd) {
			canEnd = false;
			StartCoroutine (EndLevel ());
		}

	}
}
