using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour {
	
	public float xDestination;
	public float yDestination;
	public GameObject platform;
	private bool functionCalled = false;
	private bool goingRight = false;
	private bool goingLeft = false;
	private bool goingUp = false;
	private bool goingDown = false;
	private int calls = 0;
	private bool buttonPressed = false;


	void OnTriggerEnter2D(Collider2D other){
		if (functionCalled == false) {
			if (other.gameObject.CompareTag ("Weight") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag ("parabolaGweep") ) {
				buttonPressed = true;

				if (platform.GetComponent<platformListener> ().GetPlayerOn ()) {
					movePlatform ();
				}

			} 
		}
	}
		
		


	void Update(){
		if (buttonPressed && functionCalled == false && platform.GetComponent<platformListener> ().GetPlayerOn ()) {
			movePlatform ();
		}


		if (calls == 1) {
			return;
		}
		if (goingRight) {
			if (goingUp) {
				if (platform.transform.position.x >= xDestination && platform.transform.position.y >= yDestination) {
					platform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
					calls += 1;
				}
			} else if (goingDown) {
				if (platform.transform.position.x >= xDestination && platform.transform.position.y <= yDestination) {
					platform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
					calls += 1;
				}
			} else {
				if (platform.transform.position.x >= xDestination && platform.transform.position.y == yDestination) {
					platform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
					calls += 1;
				}
			}

		} else if (goingLeft) {
			if (goingUp) {
				if (platform.transform.position.x <= xDestination && platform.transform.position.y >= yDestination) {
					platform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
					calls += 1;
				}
			} else if (goingDown) {
				if (platform.transform.position.x <= xDestination && platform.transform.position.y <= yDestination) {
					platform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
					calls += 1;
				}
			} else {
				if (platform.transform.position.x <= xDestination && platform.transform.position.y == yDestination) {
					platform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
					calls += 1;
				}
			}
		} else {
			if (goingUp) {
				if (platform.transform.position.x == xDestination && platform.transform.position.y >= yDestination) {
					platform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
					calls += 1;
				}
			} else if (goingDown) {
				if (platform.transform.position.x == xDestination && platform.transform.position.y <= yDestination) {
					platform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
					calls += 1;
				}
			} else {
				if (platform.transform.position.x == xDestination && platform.transform.position.y == yDestination) {
					platform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
					calls += 1;
				}
			}
		}


	}

	void movePlatform(){
		functionCalled = true;
		float xDistance = xDestination - platform.transform.position.x;
		float yDistance = yDestination - platform.transform.position.y;
		float xVelocity;
		float yVelocity;

		if (xDistance > 0f) {
			xVelocity = 3f;
			goingRight = true;
		} else if (xDistance < 0f) {
			xVelocity = -3f;
			goingLeft = true;
		} else {
			xVelocity = 0f;
		}
		if (yDistance > 0f) {
			yVelocity = 3f;
			goingUp = true;
		} else if (yDistance < 0f) {
			yVelocity = -3f;
			goingDown = true;
		} else {
			yVelocity = 0f;
		}
		platform.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, yVelocity);

	}



}
