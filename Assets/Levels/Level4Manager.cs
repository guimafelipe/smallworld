using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4Manager : MonoBehaviour {

	public Plot4 plot;
	public GameObject player, world, platform, mCamera, partner;
	public bool exitIntro = false, startedCollapse = false, worldDestroyed = false, canEnd = false;


	// Use this for initialization
	void Start () {
		//Stard fade in effect
		plot = GameObject.Find ("Plot4").GetComponent<Plot4> ();
		player = GameObject.Find ("Player");
		partner = GameObject.Find ("Partner");
		mCamera = GameObject.Find ("Main Camera");
		plot.currLine = 0;
		world = GameObject.Find ("World");
		//platform = GameObject.Find ("Platform");
		platform.SetActive (false);
		StartCoroutine (StartFirstPlot ());
	}

	IEnumerator StartFirstPlot(){
		player.GetComponent<PlayerController> ().DontLetMove ();
		yield return new WaitForSeconds (1f);
		plot.StartedPlot (0, 1);
		//plot.currLine++;
	}

	IEnumerator Collapse(){
		player.GetComponent<PlayerController> ().DontLetMove ();
		//mCamera.GetComponent<CamShakeEffect> ().shakeThat = true;
		yield return new WaitForSeconds(1.3f);
		platform.SetActive (true);
		partner.GetComponent<PartnerBehaviour> ().hSpeed = 2;
		yield return new WaitForSeconds (0.5f);
		player.GetComponent<PlayerController> ().canMove = true;
		yield return new WaitForSeconds (0.5f);
		partner.GetComponent<PartnerBehaviour> ().SetMovDir (-1);
		yield return new WaitForSeconds (0.3f);
		partner.GetComponent<PartnerBehaviour> ().Jump (5f);
		yield return new WaitForSeconds (0.7f);
		partner.GetComponent<PartnerBehaviour> ().SetMovDir (0);

	}

	IEnumerator DestroyWorld(){
		partner.GetComponent<PartnerBehaviour> ().SetMovDir (0);
		partner.GetComponentInChildren<Collider2D> ().enabled = false;
		player.GetComponent<PlayerController> ().DontLetMove ();
		world.SetActive (false);
		Debug.Log ("desativou mundo");
		yield return new WaitForSeconds (2f);
		world.SetActive (true);
		yield return new WaitForSeconds (1f);
		platform.SetActive (false);
		player.GetComponent<PlayerController> ().ExitFloor ();
		//player.GetComponent<PlayerController> ().canMove = true;
		yield return new WaitForSeconds (1f);
		plot.StartedPlot (1, 6);
	}

	IEnumerator RestartLevel(){
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (4);
	}

	IEnumerator EndLevel(){
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (0);
	}

	// Update is called once per frame
	void Update () {

		if (player.GetComponent<PlayerController> ().isDead) {
			StartCoroutine (RestartLevel ());
		}

		if (exitIntro && !startedCollapse) {
			startedCollapse = true;
			StartCoroutine (Collapse ());
		}

		if (platform.GetComponent<Platform> ().playerReached && !worldDestroyed) {
			worldDestroyed = true;
			StartCoroutine (DestroyWorld ());
		}

		if (canEnd) {
			canEnd = false;
			StartCoroutine (EndLevel ());
		}
	}
}
