using UnityEngine;
using System.Collections;

public class ConstantGweepCollision : MonoBehaviour {



	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Obstacle")) {
			Destroy (other.gameObject);
			Destroy (gameObject);
		} else if (other.gameObject.CompareTag ("Unbreakable")) {
			Destroy (gameObject);
		}
	}

}
