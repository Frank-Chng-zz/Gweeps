using UnityEngine;
using System.Collections;

public class Level05Button : MonoBehaviour {
	float time = 3f;
	private bool triggered = false;
	public GameObject block1, block2, block3, block4;
	private AudioSource tickingSound;
	private int playCount = 0;
	private bool destructed = false;

	// Use this for initialization
	void Start () {
		tickingSound = GameObject.FindGameObjectWithTag ("Audio").transform.Find ("Ticking").gameObject.GetComponent<AudioSource>();
	}

	void Update(){
		if (triggered) {
			time -= Time.deltaTime;
			if (destructed == false) {
				if (block1) {
					block1.GetComponent<SpriteRenderer>().color = new Color (1f, 0f, 0f, 1f);
				}

				if (block2) {
					block2.GetComponent<SpriteRenderer>().color = new Color (1f, 0f, 0f, 1f);
				}


				if (block3) {
					block3.GetComponent<SpriteRenderer>().color = new Color (1f, 0f, 0f, 1f);
				}

				if (block4) {
					block4.GetComponent<SpriteRenderer>().color = new Color (1f, 0f, 0f, 1f);
				}

			}


			if (time <= 3f && playCount == 0) {
				tickingSound.Play ();
				playCount += 1;

			}
			if (time <= 2f && playCount == 1) {
				tickingSound.Play ();
				playCount += 1;
			}
			if (time <= 1f && playCount == 2) {
				tickingSound.Play ();
				playCount += 1;
			}
			if (time <= 0f) {
				destructed = true;
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
