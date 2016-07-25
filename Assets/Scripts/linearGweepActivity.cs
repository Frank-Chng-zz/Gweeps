using UnityEngine;
using System.Collections;

public class linearGweepActivity : MonoBehaviour {
	float lifeSpan = 3f;

	void Update(){
		lifeSpan -= Time.deltaTime;
		if (lifeSpan <= 0) {
			Destroy (gameObject);
		}
	}

		


}
