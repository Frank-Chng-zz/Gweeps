using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using UnityEngine.UI;

public class ProjectilePreview : MonoBehaviour {

	private PlayerController playerController;
	private PlatformerCharacter2D PC2D;
	private string currentGweep; 
	private bool facingRight;
	private Transform leftShot;
	private Transform rightShot;
	private Vector3 position;
	private Vector3 velocity;
	public GameObject projectileDot;
	private Rigidbody2D rb;
	public LinearEquationInteractable LEI;
	public int numLineDots;

	public Slider slider;
	private bool ShowingLine;

	private float delayTime;

	void Start () {
		playerController = GetComponent<PlayerController>();
		PC2D = GetComponent<PlatformerCharacter2D> ();
		leftShot = PC2D.leftShot;
		rightShot = PC2D.rightShot;
		rb = GetComponent<Rigidbody2D> ();
		delayTime = 1f;
		ShowingLine = false;
		numLineDots = 25;

	}
	
	// Update is called once per frame
	void Update () {
		currentGweep = playerController.getCurrentGweep ();
		facingRight = PC2D.facingRight ();

		if (ShowingLine == true) {
			if (rb.velocity.magnitude > 0) {
				DeleteDots ();
				ShowingLine = false;
			} //else if (currentGweep != "constantGweep") {
				//slider.onValueChanged.AddListener (delegate {
				//	DeleteDots ();
				//	ShowingLine = false;	
				//});
		//	}
		}
		else if (currentGweep != null && rb.velocity.magnitude == 0 && ShowingLine == false) {
			if (currentGweep == "constantGweep") {
				delayTime -= Time.deltaTime;
				if (delayTime <= 0f) {
					if (facingRight) {
						UpdateCGTrajectory ("Right");

					} else {
						UpdateCGTrajectory ("Left");
					}
					ShowingLine = true;
					delayTime = 1f;
				}
			}

			if (currentGweep == "linearGweep") {
				delayTime -= Time.deltaTime;
				if (delayTime <= 0f) {
					if (facingRight) {
						UpdateLinTrajectory ("Right");
					} else {
						UpdateLinTrajectory ("Left");
					}
					delayTime = 1f;
					ShowingLine = true;
				}
			}
		}
	}


	void UpdateCGTrajectory (string direction) {
		
		int numSteps = numLineDots;
		float timeDelta = 0.02f;
		if (direction == "Right") {
			position = rightShot.position;
			velocity = new Vector3 (20f, 0, 0);
		} else {
			position = leftShot.position;
			velocity = new Vector3 (-20f, 0, 0);
		}

		for (int i = 0; i < numSteps; ++i) {
			Instantiate (projectileDot, position, Quaternion.identity);
			position += velocity * timeDelta;
		}
	}
		
	void UpdateLinTrajectory(string direction){
		int numSteps = numLineDots;
		float timeDelta = 0.02f;
		float slope = LEI.getSlopeValue();
		if (direction == "Right") {
			position = rightShot.position;
			float x = (float)Math.Sqrt((float)(400/((slope*slope) + 1)));
			velocity = new Vector3 (x, x * slope, 0);
		} else {
			position = leftShot.position;
			float x = (float)Math.Sqrt((float)(400/((slope*slope) + 1)));
			velocity = new Vector3 (-x, x * slope, 0);
		}

		for (int i = 0; i < numSteps; ++i) {
			Instantiate (projectileDot, position, Quaternion.identity);
			position += velocity * timeDelta;
		}
	}

	void DeleteDots(){
		
		foreach(GameObject Dot in GameObject.FindGameObjectsWithTag("projectileDot")){
			Destroy (Dot);
		}


	}

}
