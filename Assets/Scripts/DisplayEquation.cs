using UnityEngine;
using System.Collections;

public class DisplayEquation : MonoBehaviour {

	private PlayerController playerController;

	private GameObject ConstantGweepEquation;
	private GameObject LinearGweepEquation;
	private GameObject ParabolaGweepEquation;

	private GameObject currentGweepEquation;
	
	void Start(){
		GameObject player = GameObject.FindWithTag ("Player");
		if (player == null) {
			Debug.Log ("No player in scene!");
		} else{
			playerController = player.GetComponent<PlayerController>();
		}

		ConstantGweepEquation = transform.Find ("ConstantGweepEquation").gameObject;
		LinearGweepEquation = transform.Find ("LinearGweepEquation").gameObject;
		ParabolaGweepEquation = transform.Find ("ParabolaGweepEquation").gameObject;
	}


	void Update () {
		if (playerController.getCurrentGweep () != null) {
			string currentGweep = playerController.getCurrentGweep ();

			if (currentGweep == "constantGweep") {
				
				//turn off the current equation 
				if (currentGweepEquation != null) {
					currentGweepEquation.SetActive (false);
					currentGweepEquation = null;
				}
				currentGweepEquation = ConstantGweepEquation;
				currentGweepEquation.SetActive (true);
			} else if (currentGweep == "linearGweep") {
			
				if (currentGweepEquation != null) {
					currentGweepEquation.SetActive (false);
					currentGweepEquation = null;
				}
				currentGweepEquation = LinearGweepEquation;
				currentGweepEquation.SetActive (true);

			} else if(currentGweep == "parabolaGweep"){
				if (currentGweepEquation != null) {
					currentGweepEquation.SetActive (false);
					currentGweepEquation = null;
				}
				currentGweepEquation = ParabolaGweepEquation;
				currentGweepEquation.SetActive (true);
			}



		} else {
			if (currentGweepEquation != null) {
				currentGweepEquation.SetActive (false);
				currentGweepEquation = null;
			}

		}

	}
}
