using UnityEngine;
using System.Collections;

public class linearGweepActivity : MonoBehaviour {
	int bounces = 15;

	void OnCollisionEnter2D(Collision2D other){
		bounces -= 1;
		if (bounces == 0) {
			Destroy (gameObject);
		}
	}
}
