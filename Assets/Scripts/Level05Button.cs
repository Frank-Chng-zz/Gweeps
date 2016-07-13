using UnityEngine;
using System.Collections;

public class Level05Button : MonoBehaviour {
	float time = 3f;
	private bool triggered = false;
	public GameObject block1, block2, block3, block4;

	// Use this for initialization
	void Start () {
	
	}

	void Update(){
		if (triggered) {
			time -= Time.deltaTime;
			if (time <= 0f) {
				Destroy (block1);
				Destroy (block2);
				Destroy (block3);
				Destroy (block4);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Weight") || other.gameObject.CompareTag("Player")) {
			triggered = true;
		} else if (other.gameObject.CompareTag ("parabolaGweep")) {
			triggered = true;
		}
	}
}
