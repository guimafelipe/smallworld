using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot1 : MonoBehaviour {

	public GameObject player;
	public GameObject level1manager;
	private PlayerController playerController;
	private Level1Manager levelmanager;
	public string[] plot;

	public int currLine = 0, currMaxLine = 50, currMaxInd;
	private bool isSaying;

	//private int[] parts = { 10, 13, 15, 19, 20 };
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		level1manager = GameObject.Find ("Level1Manager");
		playerController = player.GetComponent<PlayerController> ();
		levelmanager = level1manager.GetComponent<Level1Manager> ();
		currMaxLine = 50;
		//currMaxLine = parts [currMaxInd];
		//BuildPlot();
		//StartedPlot();

	}

	// Update is called once per frame
	void Update () {
		if (isSaying && Input.GetKeyDown (KeyCode.Space)) {
			SayPlot (++currLine);
			//currLine+;
		} 
		if(currLine > 9)
			levelmanager.exitIntro = true;
		if (currLine > 21)
			levelmanager.canEnd = true;
		if (currLine == currMaxLine) {
			EndedPlot ();
			currLine++; //ToCallJustoOneTime;
			//levelmanager.
			//currMaxLine = parts [++currMaxInd];
		}
	}
		
	IEnumerator waitSeconds(float x){
		yield return new WaitForSeconds(x);
	}

	public void StartedPlot(int i, int f){
		isSaying = true;
		currLine = i;
		currMaxLine = f;
		playerController.SayLine (plot[currLine]);
	}

	public void SayPlot(int i){
		player.GetComponent<PlayerController>().SayLine (plot [i]);
		isSaying = true;
	}

	public void EndedPlot(){
		Debug.Log ("Chamou ended plot");
		isSaying = false;
		playerController.EndLine ();
		playerController.canJump = true;
		playerController.canMove = true;
	}
}
