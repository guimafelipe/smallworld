using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour {

	public Plot3 plot;
	public GameObject player, partner;
	public GameObject platforms, moon;
	public bool exitIntro = false, canFall = false, canEnd = false;

	private bool partnerReachedMoon = false, partnerStartedToGo = false, startedToFall = false, finallyFall = false;

	// Use this for initialization
	void Start () {
		//Stard fade in effect
		plot = GameObject.Find ("Plot3").GetComponent<Plot3> ();
		player = GameObject.Find ("Player");
		partner = GameObject.Find ("Partner");
		platforms = GameObject.Find ("Platforms");
		moon = GameObject.Find ("Moon");
		plot.currLine = 0;
		StartCoroutine (StartFirstPlot ());
	}

	IEnumerator StartFirstPlot(){
		player.GetComponent<PlayerController> ().DontLetMove ();
		yield return new WaitForSeconds (1f);
		plot.StartedPlot (0, 1);
	}

	IEnumerator GoToTheMoon(){
		float jumpSpeed = 5f;
		float cdTime = 1f;
		partnerStartedToGo = true;
		partner.GetComponent<PartnerBehaviour> ().SetMovDir (-1f);
		partner.GetComponent<PartnerBehaviour> ().Jump (jumpSpeed);
		yield return new WaitForSeconds (cdTime);
		partner.GetComponent<PartnerBehaviour> ().Jump (jumpSpeed);
		yield return new WaitForSeconds (cdTime);
		partner.GetComponent<PartnerBehaviour> ().SetMovDir (1f);
		partner.GetComponent<PartnerBehaviour> ().Jump (jumpSpeed);
		yield return new WaitForSeconds (cdTime);
		partner.GetComponent<PartnerBehaviour> ().Jump (jumpSpeed);
		yield return new WaitForSeconds (cdTime);
		partner.GetComponent<PartnerBehaviour> ().SetMovDir (-1f);
		partner.GetComponent<PartnerBehaviour> ().Jump (jumpSpeed);
		yield return new WaitForSeconds (cdTime);
		partner.GetComponent<PartnerBehaviour> ().Jump (jumpSpeed);
		yield return new WaitForSeconds (cdTime);
		partner.GetComponent<PartnerBehaviour> ().SetMovDir (1f);
		partner.GetComponent<PartnerBehaviour> ().Jump (jumpSpeed);
		yield return new WaitForSeconds (cdTime);
		partner.GetComponent<PartnerBehaviour> ().SetMovDir (0f);
		partnerReachedMoon = true;
	}

	IEnumerator StartToFall(){
		
		platforms.SetActive (false);
		yield return new WaitForSeconds (1f);
		moon.SetActive (false);
		player.GetComponent<PlayerController> ().ExitFloor ();
		partner.GetComponent<PartnerBehaviour> ().ExitFloor ();
		yield return new WaitForSeconds (1.5f);
		plot.StartedPlot (2, 6);

	}

	IEnumerator EndLevel(){
		yield return 0f;
	}

	// Update is called once per frame
	void Update () {
		if (exitIntro && !partnerReachedMoon && !partnerStartedToGo) {
			StartCoroutine (GoToTheMoon ());
		}
		if (moon && moon.GetComponent<Moon> ().playerReached && !startedToFall) {
			startedToFall = true;
			player.GetComponent<PlayerController> ().DontLetMove ();
			plot.StartedPlot (1, 2);
		}
		if (canFall && !finallyFall) {
			finallyFall = true;
			StartCoroutine (StartToFall ());
		}

		if (canEnd) {
			StartCoroutine (EndLevel ());
			canEnd = false;
		}
			
	}
}
