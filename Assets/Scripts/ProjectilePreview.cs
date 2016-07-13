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

	//private Rigidbody2D rb;
	public LinearEquationInteractable LEI;
	public QuadraticEquationInteractable QEI;
	private CalculateParabola CP;

	public int numLineDots;
	private float delayTime;
	private bool ShowingLine;

	//public LineRenderer lineRenderer;

	void Start () {
		playerController = GetComponent<PlayerController>();
		PC2D = GetComponent<PlatformerCharacter2D> ();
		leftShot = PC2D.leftShot;
		rightShot = PC2D.rightShot;
		//rb = GetComponent<Rigidbody2D> ();
		delayTime = 0.25f;
		ShowingLine = false;
		numLineDots = 25;
		CP = GetComponent<CalculateParabola> ();
		//lineRenderer = GetComponent<LineRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		currentGweep = playerController.getCurrentGweep ();
		facingRight = PC2D.facingRight ();

		if (ShowingLine == true) {
			if (Input.GetButtonDown ("Fire1")) {
				DeleteDots ();
				ShowingLine = false;
			}
		} else if (currentGweep != null && ShowingLine == false) {
			delayTime -= Time.deltaTime;
			if (delayTime <= 0f) {
				
				if (currentGweep == "constantGweep") {
					if (facingRight) {
						UpdateCGTrajectory ("Right");
					} else {
						UpdateCGTrajectory ("Left");
					}
				}

				if (currentGweep == "linearGweep") {
					if (facingRight) {
						UpdateLinTrajectory ("Right");
					} else {
						UpdateLinTrajectory ("Left");
					}
				}

				if (currentGweep == "parabolaGweep") {
					if (facingRight) {
						UpdateQuadraticTrajectory ("Right");
					} else {
						UpdateQuadraticTrajectory ("Left");
					}
				}
				delayTime = 0.25f;
				ShowingLine = true;
			}
		}
	}

	public void UpdateCGTrajectory (string direction) {
		
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
			GameObject dot = (GameObject)Instantiate (projectileDot, position, Quaternion.identity);
			dot.transform.parent = gameObject.transform;
			position += velocity * timeDelta;
		}

		/*lineRenderer.SetVertexCount (numLineDots);
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
			lineRenderer.SetPosition (i, position);
			position += velocity * timeDelta;
		}
		*/

	}
		
	public void UpdateLinTrajectory(string direction){
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
			GameObject dot = (GameObject) Instantiate (projectileDot, position, Quaternion.identity);
			dot.transform.parent = gameObject.transform;
			position += velocity * timeDelta;
		}
	}

	public void UpdateQuadraticTrajectory(string direction){
		int numSteps = numLineDots;
		float linearCoef = QEI.getLinearValue ();
		float quadraticCoef = QEI.getQuadraticValue ();

		float leftRoot = CP.calculateLeftRoot (linearCoef, quadraticCoef);
		float rightRoot = CP.calculateRightRoot (linearCoef, quadraticCoef);
		float stepDistance = (float)((rightRoot - leftRoot) / numSteps);



		if (direction == "Right") {
			GameObject initDot = (GameObject)Instantiate (projectileDot, rightShot.position, Quaternion.identity);
			initDot.transform.parent = gameObject.transform;
			for (int i = 1; i < numSteps; ++i) {
				float xDisplacement = i * stepDistance;
				float yDisplacement = CP.quadraticFunction (leftRoot + xDisplacement);
				GameObject dot = (GameObject)Instantiate(projectileDot, rightShot.position + new Vector3(xDisplacement, yDisplacement, 0f), Quaternion.identity);
				dot.transform.parent = gameObject.transform;

			}
		} else {
			GameObject initDot = (GameObject)Instantiate (projectileDot, leftShot.position, Quaternion.identity);
			initDot.transform.parent = gameObject.transform;
			for (int i = 1; i < numSteps; ++i) {
				float xDisplacement = i * stepDistance;
				float yDisplacement = CP.quadraticFunction (leftRoot + xDisplacement);
				GameObject dot = (GameObject)Instantiate(projectileDot, rightShot.position + new Vector3(-xDisplacement, yDisplacement, 0f), Quaternion.identity);
				dot.transform.parent = gameObject.transform;

			}
		}

	}

	public void DeleteDots(){
		foreach(GameObject Dot in GameObject.FindGameObjectsWithTag("projectileDot")){
			Destroy (Dot);
		}
	}

	public void setShowingLine(bool showing){
		ShowingLine = showing;
	}

}
