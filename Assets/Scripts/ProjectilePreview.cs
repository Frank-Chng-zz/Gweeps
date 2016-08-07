using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	private bool LineOn;

	private bool ShowingLine;


	void Start () {
		playerController = GetComponent<PlayerController>();
		PC2D = GetComponent<PlatformerCharacter2D> ();
		leftShot = transform.Find ("leftShot");
		rightShot = transform.Find ("rightShot");
		//rb = GetComponent<Rigidbody2D> ();
		ShowingLine = false;
		numLineDots = 25;
		CP = GetComponent<CalculateParabola> ();
		if (SceneManager.GetActiveScene ().buildIndex >= 9) {
			LineOn = false;
		} else {
			LineOn = true;
		}
	}

	void Update () {
		currentGweep = playerController.getCurrentGweep ();
		facingRight = PC2D.facingRight ();
		if(LineOn == false){
			return;
		}
		if (ShowingLine == true) {
			if (Input.GetButtonDown ("Fire1")) {
				DeleteDots ();
				ShowingLine = false;
			} else {
				DeleteDots ();
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
			}

		} else if (currentGweep != null && ShowingLine == false) {

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
			ShowingLine = true;
		}


	}

	public void UpdateCGTrajectory (string direction) {
		if(LineOn == false){
			return;
		}
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

			bool touching = false;
			if (Physics.OverlapSphere (position, 0.48f) != null) {
				foreach (Collider2D coll in Physics2D.OverlapCircleAll(new Vector2(position.x, position.y), 0.48f)){
					if ((coll.gameObject.CompareTag ("Obstacle") || coll.gameObject.CompareTag("Unbreakable") || coll.gameObject.CompareTag("Weight"))) {
						touching = true;
						break;
					}
				}
			}
			if (touching) {
				break;
			}

			GameObject dot = (GameObject)Instantiate (projectileDot, position, Quaternion.identity);
			dot.transform.parent = gameObject.transform;
			position += velocity * timeDelta;
		}

	}
		
	public void UpdateLinTrajectory(string direction){
		if(LineOn == false){
			return;
		}
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
			bool touching = false;
			if (Physics.OverlapSphere (position, 0.48f) != null) {
				foreach (Collider2D coll in Physics2D.OverlapCircleAll(new Vector2(position.x, position.y), 0.48f)){
					if ((coll.gameObject.CompareTag ("Obstacle") || coll.gameObject.CompareTag("Unbreakable") || coll.gameObject.CompareTag("Weight"))) {
						touching = true;
						break;
					}
				}
			}
			if (touching) {
				break;
			}

			GameObject dot = (GameObject) Instantiate (projectileDot, position, Quaternion.identity);
			dot.transform.parent = gameObject.transform;
			position += velocity * timeDelta;
		}
	}

	public void UpdateQuadraticTrajectory(string direction){
		if(LineOn == false){
			return;
		}
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
				position = rightShot.position + new Vector3 (xDisplacement, yDisplacement, 0f);

				bool touching = false;
				if (Physics.OverlapSphere (position, 0.52f) != null) {
					foreach (Collider2D coll in Physics2D.OverlapCircleAll(new Vector2(position.x, position.y), 0.52f)){
						if ((coll.gameObject.CompareTag ("Obstacle") || coll.gameObject.CompareTag("Unbreakable") || coll.gameObject.CompareTag("Weight"))) {
							touching = true;
							break;
						}
					}
				}
				if (touching) {
					break;
				}
				GameObject dot = (GameObject)Instantiate(projectileDot, position, Quaternion.identity);
				dot.transform.parent = gameObject.transform;

			}
		} else {
			GameObject initDot = (GameObject)Instantiate (projectileDot, leftShot.position, Quaternion.identity);
			initDot.transform.parent = gameObject.transform;
			for (int i = 1; i < numSteps; ++i) {
				float xDisplacement = i * stepDistance;
				float yDisplacement = CP.quadraticFunction (leftRoot + xDisplacement);
				position = leftShot.position + new Vector3 (-xDisplacement, yDisplacement, 0f);

				bool touching = false;
				if (Physics.OverlapSphere (position, 0.52f) != null) {
					foreach (Collider2D coll in Physics2D.OverlapCircleAll(new Vector2(position.x, position.y), 0.52f)){
						if ((coll.gameObject.CompareTag ("Obstacle") || coll.gameObject.CompareTag("Unbreakable"))) {
							touching = true;
							break;
						}
					}
				}
				if (touching) {
					break;
				}

				GameObject dot = (GameObject)Instantiate(projectileDot, position, Quaternion.identity);
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
