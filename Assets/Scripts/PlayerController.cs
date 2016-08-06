using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	private bool hasKey;
	private AudioSource keyPickUpSound;
	private AudioSource GweepPickUpSound;
	private GameObject boundary;
	private Rigidbody2D rb;
	//private Transform playerTransform;
	//public AudioSource levelWinSound;
	private float min_X, max_X, min_Y, max_Y;

	//To get the number of Gweeps, we will make a variable for the quantity of each Gweep
	//and we will acquire the quantities from a unique object in each scene
	//these quantities and Gweeps will then be shown in the UI.
	//These quantities can also be incremented by picking up Gweeps

	public Sprite constantGweepSprite;
	private int constantGweeps = 0;
	public Sprite linearGweepSprite;
	private int linearGweeps = 0;
	private int parabolaGweeps = 0;
	public Sprite parabolaGweepSprite;


	private string currentGweep;
	private ProjectilePreview PP;
	private Image GweepImage;


	void Start () {
		boundary = GameObject.FindGameObjectWithTag ("Boundary");
		hasKey = false;
		rb = GetComponent<Rigidbody2D> ();
		min_X = boundary.transform.position.x - ((float)boundary.transform.localScale.x/2);
		max_X = boundary.transform.position.x + ((float)boundary.transform.localScale.x/2);
		//playerTransform = GetComponent<Transform> ();
		currentGweep = null;
		PP = GetComponent<ProjectilePreview> ();
		if (GameObject.FindGameObjectWithTag ("Audio") != null) {
			keyPickUpSound = GameObject.FindGameObjectWithTag ("Audio").transform.Find ("KeyPickUp").gameObject.GetComponent<AudioSource>();
			GweepPickUpSound = GameObject.FindGameObjectWithTag ("Audio").transform.Find ("CollectGweep").gameObject.GetComponent<AudioSource>();
		}

		GweepImage = GameObject.FindGameObjectWithTag ("HUDCanvas").transform.Find ("GweepImage").gameObject.GetComponent<Image> ();
	}
		
	void Update(){
		//Restarting game
		if (Input.GetKeyDown ("r")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}

	}

	void FixedUpdate () {
		rb.position = new Vector2 (
			Mathf.Clamp (rb.position.x, min_X + 0.3f, max_X - 0.3f),
			rb.position.y
		);
	}

	void OnTriggerEnter2D(Collider2D other){

		//Picking up a key
		if (other.gameObject.CompareTag("Key")){
			keyPickUpSound.Play ();
			other.gameObject.SetActive (false);
			hasKey = true;
		}
	
		//Entering a door
		if (other.gameObject.CompareTag ("Door")) {
			if (hasKey) {
				int i = SceneManager.GetActiveScene ().buildIndex;
				SceneManager.LoadScene (i + 1);
				hasKey = false;
			}
		}

		//Touching spikes
		if (other.gameObject.CompareTag("Spikes")){
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}

		//Picking up constantGweepPickUps
		if (other.gameObject.CompareTag ("constantGweepPickUp")) {
			constantGweeps = constantGweeps + 1;
			other.gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			other.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			//possibly temporary
			setCurrentGweep("constantGweep");
			GweepPickUpSound.Play ();
			PP.DeleteDots ();
			PP.setShowingLine (false);
		}

		if (other.gameObject.CompareTag ("linearGweepPickUp")) {
			linearGweeps = linearGweeps + 1;
			other.gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			other.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			setCurrentGweep ("linearGweep");
			GweepPickUpSound.Play ();
			PP.DeleteDots();
			PP.setShowingLine (false);
		}

		if (other.gameObject.CompareTag ("parabolaGweepPickUp")) {
			parabolaGweeps = parabolaGweeps + 1;
			other.gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			other.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			setCurrentGweep ("parabolaGweep");
			GweepPickUpSound.Play ();
			PP.DeleteDots ();
			PP.setShowingLine (false);
		}

	}

	//it might be useful to seperate these functions for Gweep firing into another script called PlayerShooting
	//for now, they'll stay in this script, but in the future - compartmentalize the code

	public void setCurrentGweep(string GweepName){
		currentGweep = GweepName;

		if (currentGweep == "null") {
			currentGweep = null;
		} else if (currentGweep == "constantGweep") {
			GweepImage.sprite = constantGweepSprite;

			//GweepEquation = 
		} else if (currentGweep == "linearGweep") {
			GweepImage.sprite = linearGweepSprite;

			//GweepEquation = 
		} else if (currentGweep == "parabolaGweep") {
			GweepImage.sprite = parabolaGweepSprite;
		}

	}

	public string getCurrentGweep(){
		return currentGweep;
		
	}

	public void setConstantGweeps(int cG){
		constantGweeps = cG;
	}

	public int getConstantGweeps(){
		return constantGweeps;
	}

	public void setLinearGweeps(int lG){
		linearGweeps = lG;
	}

	public int getLinearGweeps(){
		return linearGweeps;
	}

	public void setParabolaGweeps(int pG){
		parabolaGweeps = pG;
	}

	public int getParabolaGweeps(){
		return parabolaGweeps;
	}

}
