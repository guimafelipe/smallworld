using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot3 : MonoBehaviour {

	public GameObject player;
	public GameObject level3manager;
	private PlayerController playerController;
	private Level3Manager levelmanager;
	public string[] plot;
	public int currLine = 0, currMaxLine = 50, currMaxInd;
	private bool isSaying;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		level3manager = GameObject.Find ("Level3Manager");
		playerController = player.GetComponent<PlayerController> ();
		levelmanager = level3manager.GetComponent<Level3Manager> ();
		currMaxLine = 50;
	}
	
	// Update is called once per frame
	void Update () {
		if (isSaying && Input.GetKeyDown (KeyCode.Space)) {
			SayPlot (++currLine);
		} 
		if(currLine > 1)
			levelmanager.exitIntro = true;
		if (currLine > 2)
			levelmanager.canFall = true;
		if (currLine > 6)
			levelmanager.canEnd = true;
		if (currLine == currMaxLine) {
			EndedPlot ();
			currLine++; //ToCallJustoOneTime;
		}
	}

	public void StartedPlot(int i, int f){
		Debug.Log("Chamou aqui");
		isSaying = true;
		currLine = i;
		currMaxLine = f;
		playerController.SayLine (plot[currLine]);
	}

	public void SayPlot(int i){
		Debug.Log ("Plotou i");
		player.GetComponent<PlayerController>().SayLine (plot [i]);
		isSaying = true;
	}

	public void EndedPlot(){
		isSaying = false;
		playerController.EndLine ();
		playerController.canJump = true;
		playerController.canMove = true;
	}
}
