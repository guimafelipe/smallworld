using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour {

	public Plot1 plot;
	public GameObject player;
	public bool[] partEnded = {false, false, false, false};
	public GameObject flowerPrefab;
	public bool exitIntro = false; 
	public bool gotFlower1 = false, createdFlower1 = false;
	public bool gotFlower2 = false, createdFlower2 = false;
	private GameObject flower;
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
		plot.StartedPlot (0);
		//plot.currLine++;
	}

	IEnumerator SpawnFirstFlower(){
		player.GetComponent<PlayerController> ().DontLetMove ();
		yield return new WaitForSeconds (1.5f);
		flower = Instantiate (flowerPrefab, new Vector3 (4.7f, -1f, 0f), Quaternion.identity);
		createdFlower1 = true;
		plot.StartedPlot (10);
	}

	// Update is called once per frame
	void Update () {



		//Spike spike;
		if (exitIntro && !createdFlower1) {
			StartCoroutine (SpawnFirstFlower());
			createdFlower1 = true;
		}
		if (flower != null && flower.GetComponent<Flower>().gotCaught && !gotFlower1) {
			Destroy (flower);
			gotFlower1 = true;
		}
		if (gotFlower1 && !createdFlower2) {
			flower = Instantiate (flowerPrefab, new Vector3 (-4.7f, -1f, 0f), Quaternion.identity);
			createdFlower2 = true;
		}
	}
}
