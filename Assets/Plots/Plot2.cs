using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot2 : MonoBehaviour {

	public GameObject player;
	public GameObject level2manager;
	private PlayerController playerController;
	private Level2Manager levelmanager;
	public string[] plot;
	public int currLine = 0, currMaxLine = 50, currMaxInd;
	private bool isSaying;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		level2manager = GameObject.Find ("Level2Manager");
		playerController = player.GetComponent<PlayerController> ();
		levelmanager = level2manager.GetComponent<Level2Manager> ();
		currMaxLine = 50;
	}
	
	// Update is called once per frame
	void Update () {
		if (isSaying && Input.GetKeyDown (KeyCode.Space)) {
			SayPlot (++currLine);
		} 
		if(currLine > 4)
			levelmanager.exitIntro = true;
		if (currLine == currMaxLine) {
			Debug.Log ("Entoru aqui");
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
