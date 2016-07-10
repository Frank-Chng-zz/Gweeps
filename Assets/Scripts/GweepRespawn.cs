using UnityEngine;
using System.Collections;

public class GweepRespawn : MonoBehaviour {

	private float respawnTime = 3f;
	private bool pickedUp = false;
	private GameObject Gweep;
	private Transform GweepTransform;

	void Start(){
		Gweep = gameObject;
		GweepTransform = Gweep.transform;
	}

	// Update is called once per frame
	void Update () {
		if (pickedUp == true) {
			respawnTime -= Time.deltaTime;
			if (respawnTime <= 0f) {
				Instantiate (Gweep, GweepTransform.position, Quaternion.identity);
				Gweep.GetComponent<CircleCollider2D> ().enabled = true;
				Gweep.GetComponent<SpriteRenderer> ().enabled = true;
				respawnTime = 3f;
				pickedUp = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			pickedUp = true;
		}
	
	}
}
