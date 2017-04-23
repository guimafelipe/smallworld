using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot1 : MonoBehaviour {

	public GameObject player;
	private PlayerController playerController;
	public string[] plot;

	private int currLine = 0, currMaxLine, currMaxInd;
	private bool isSaying;

	private int[] parts = { 10, 13, 15, 19, 20 };
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerController = player.GetComponent<PlayerController> ();
		//plot = new string[21];
		currMaxInd = 0;
		currMaxLine = parts [currMaxInd];
		//BuildPlot();
		//StartCoroutine (PlotScript1 ());
		StartedPlot();

	}

	// Update is called once per frame
	void Update () {
		if (isSaying && Input.GetKeyDown (KeyCode.Space)) {
			SayPlot (currLine);
			currLine++;
		} 
		if (currLine == currMaxLine) {
			EndedPlot ();
			currMaxLine = parts [++currMaxInd];
		}
	}

	IEnumerator PlotScript1(){
		Debug.Log ("Startou");
		for (int i = 0; i < 10; i++) {
			SayPlot (i);
			yield return new WaitForSeconds (0.5f);

		}
		yield return new WaitForSeconds (0.5f);
	}

	IEnumerator waitSeconds(float x){
		yield return new WaitForSeconds(x);
	}

	/*void BuildPlot(){
		plot [0] = "Hey, you!";
		plot [1] = "I don't have company very often.";
		plot [2] = "Where do you come from?";
		plot [3] = "I live in such a small world.";
		plot [4] = "I could cross it in a few steps";
		plot [5] = "It's always been just a piece of ground";
		plot [6] =  "and the ensless sky above.";
		plot [7] = "Can you see there's nothing to the sides?";
		plot [8] = "I'm not sure it's part of the sky";
		plot [9] = "or just a clear abyss of nothingness...";
		plot [10] = "Ooops.";
		plot [11] = "Sometimes things like this happen.";
		plot [12] = "So it's not just a plain your all time, see?";
		plot [13] = "I can always use WASD to move around";
		plot [14] = "Yep, this one also tends to happen.";
		plot [15] = "Use W to jump!";
		plot [16] = "Why do I care about moving around and doing things you ask?";
		plot [17] = "What else is there to do?";
		plot [18] = "At least, when I move";
		plot [19] = "Things stop being the same all the time.";
		plot [20] = "Even a small world is full of dellusions.";
	}*/

	void StartedPlot(){
		isSaying = true;
		playerController.SayLine (plot[currLine]);
	}

	void SayPlot(int i){
		playerController.SayLine (plot [i]);
		isSaying = true;
	}

	void EndedPlot(){
		isSaying = false;
		playerController.EndLine ();
	}
}
