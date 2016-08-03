using UnityEngine;
using System.Collections;

public class platformListener : MonoBehaviour {

	private bool playerOn = false;


	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag("Player")) {
			playerOn = true;
		}
	}


	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.CompareTag ("Player")) {
			playerOn = false;
		}
	}

	public bool GetPlayerOn(){
		return playerOn; 
	}
}
