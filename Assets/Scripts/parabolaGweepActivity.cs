using UnityEngine;
using System.Collections;

public class parabolaGweepActivity : MonoBehaviour {

	private CircleCollider2D CC2D;

	// Use this for initialization
	void Start () {
		CC2D = GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			CC2D.isTrigger = false;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if ((other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag ("Obstacle") || other.gameObject.CompareTag ("Unbreakable") || other.gameObject.CompareTag ("Weight"))) {
			Destroy (gameObject);
		}
	}
}
