using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Manager : MonoBehaviour {

	public Plot4 plot;
	public GameObject player;
	public GameObject fireflyPrefab, platformPrefab;
	public bool exitIntro = false;


	// Use this for initialization
	void Start () {
		//Stard fade in effect
		plot = GameObject.Find ("Plot4").GetComponent<Plot4> ();
		player = GameObject.Find ("Player");
		plot.currLine = 0;
		StartCoroutine (StartFirstPlot ());
	}

	IEnumerator StartFirstPlot(){
		player.GetComponent<PlayerController> ().DontLetMove ();
		yield return new WaitForSeconds (1f);
		plot.StartedPlot (0, 1);
		//plot.currLine++;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
