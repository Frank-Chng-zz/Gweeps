using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour {
	
	public double xDestination;
	public double yDestination;
	public GameObject platform;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Weight") || other.gameObject.CompareTag("Player")) {
			movePlatform ();
		} else if (other.gameObject.CompareTag ("parabolaGweep")) {
			movePlatform ();
		}
	}

	void movePlatform(){
		
	}

}
