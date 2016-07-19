using UnityEngine;
using System.Collections;

public class ButtonTrigger : MonoBehaviour {

	public GameObject buttonDown;
	private Transform buttonTransform;

	void Start(){
		buttonTransform = gameObject.GetComponent<Transform> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player") || other.gameObject.CompareTag ("Weight") || other.gameObject.CompareTag ("parabolaGweep")) {
			TriggerButton ();
		}
	
	}

	void TriggerButton(){
		Instantiate (buttonDown, buttonTransform.position, Quaternion.identity);
		gameObject.GetComponent<PolygonCollider2D> ().enabled = false;
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	}

}
