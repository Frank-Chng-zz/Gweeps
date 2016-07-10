using System;
using UnityEngine;
using UnityEngine.UI;



public class PlatformerCharacter2D : MonoBehaviour
{
    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.



	private PlayerController playerController; 
	private string currentGweep;
	public Image GweepImage;

	//leftShot and rightShot are possibly temporary, depending on whether 
	//the player can adjust where they want to start firing the Gweep
	public Transform leftShot;
	public Transform rightShot;
	//if there is a better way to do this, this is also temporary
	public GameObject constantGweep;
	public GameObject linearGweep;
	private float linearGweepSlope;
	private LinearEquationInteractable LEI;

	public GameObject parabolaGweep;
	private QuadraticEquationInteractable QEI;
	private float PGLinTerm;
	private float PGSqrTerm;
	private CalculateParabola CP;
	private GameObject HUDCanvas;


    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

		playerController = GetComponent<PlayerController> (); //referencing other script
		if (GameObject.FindGameObjectWithTag("HUDCanvas") != null){
			HUDCanvas = GameObject.FindGameObjectWithTag("HUDCanvas");
		}
		CP = GetComponent<CalculateParabola> ();
    }

	//Update is used for shooting Gweeps
	private void Update(){
		currentGweep = playerController.getCurrentGweep ();

		if (currentGweep == null) {
			GweepImage.sprite = null;
		}

		if (Input.GetButtonDown ("Fire1") && currentGweep != null) {

			//Firing constantGweeps
			if (currentGweep.Equals ("constantGweep")) {
				if (m_FacingRight) {
					fireConstantGweep ("Right");
				} else {
					fireConstantGweep ("Left");
				}
			}

			//Firing linearGweeps
			else if (currentGweep.Equals ("linearGweep")) {
				LEI = HUDCanvas.transform.FindChild ("LinearGweepEquation").GetComponent<LinearEquationInteractable> ();
				linearGweepSlope = LEI.getSlopeValue ();

				if (m_FacingRight) {
					fireLinearGweep ("Right", linearGweepSlope);
				} else {
					fireLinearGweep ("Left", linearGweepSlope);
				}
			} 

			else if (currentGweep.Equals ("parabolaGweep")) {
				QEI = HUDCanvas.transform.FindChild ("ParabolaGweepEquation").GetComponent<QuadraticEquationInteractable> ();
				PGLinTerm = QEI.getLinearValue ();
				PGSqrTerm = QEI.getQuadraticValue ();

				if (m_FacingRight) {
					fireParabolaGweep ("Right", PGLinTerm, -PGSqrTerm);
				} else {
					fireParabolaGweep ("Left", PGLinTerm, -PGSqrTerm);
				}
			}

		}
	}

	private void fireConstantGweep(string direction){

		if (playerController.getConstantGweeps() != 0){
			if (direction.Equals ("Right")) {
				GameObject cG = Instantiate (constantGweep, rightShot.position, Quaternion.identity) as GameObject;
				cG.GetComponent<Rigidbody2D> ().velocity = new Vector2 (20f, 0);
				playerController.setConstantGweeps (playerController.getConstantGweeps () - 1);
			} else {
				GameObject cG = Instantiate (constantGweep, leftShot.position, Quaternion.identity) as GameObject;
				cG.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-20f, 0);
				playerController.setConstantGweeps (playerController.getConstantGweeps () - 1);
			}
			playerController.setCurrentGweep("null");
			GweepImage.sprite = null;
		}
	}

	private void fireLinearGweep(string direction, float slope){
		if(playerController.getLinearGweeps() != 0){
			if (direction.Equals ("Right")) {
				GameObject lG = Instantiate (linearGweep, rightShot.position, Quaternion.identity) as GameObject;
				//math to ensure that the velocity of the linearGweep is always the same
				float x = (float)Math.Sqrt((float)(400/((linearGweepSlope*linearGweepSlope) + 1)));
				lG.GetComponent<Rigidbody2D> ().velocity = new Vector2 (x, x*linearGweepSlope);
				playerController.setLinearGweeps (playerController.getLinearGweeps () - 1);
			
			} else {
				GameObject lG = Instantiate (linearGweep, leftShot.position, Quaternion.identity) as GameObject;
				//math to ensure that the velocity of the linearGweep is always the same
				float x = (float)Math.Sqrt((float)(400/((linearGweepSlope*linearGweepSlope) + 1)));
				lG.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-x, x*linearGweepSlope);
				playerController.setLinearGweeps (playerController.getLinearGweeps () - 1);

			}
			playerController.setCurrentGweep("null");
			GweepImage.sprite = null;
	
		}
	}

	private void fireParabolaGweep(string direction, float linearCoef, float quadraticCoef){
		if (playerController.getParabolaGweeps () != 0) {
			float max = CP.calculateMax (linearCoef, quadraticCoef);
			float leftRoot = CP.calculateLeftRoot (linearCoef, quadraticCoef);
			float rightRoot = CP.calculateRightRoot (linearCoef, quadraticCoef);
			float time = (float)Math.Sqrt ((double)(8 * max));
			float xDistance = (rightRoot - leftRoot) * 10;
			float xForce = xDistance / time;
			float yForce = (float)Math.Sqrt ((double)(max * 2));

			if (direction.Equals ("Right")) {
				GameObject pG = Instantiate (parabolaGweep, rightShot.position + new Vector3(2f, 0, 0), Quaternion.identity) as GameObject;
				pG.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-xForce, yForce * 2); //change numbers around a bit
				playerController.setParabolaGweeps (playerController.getParabolaGweeps () - 1);
			} else {
				GameObject pG = Instantiate (parabolaGweep, leftShot.position + new Vector3(-2f, 0, 0), Quaternion.identity) as GameObject;
				pG.GetComponent<Rigidbody2D> ().velocity = new Vector2 (xForce, yForce * 2);
				playerController.setParabolaGweeps (playerController.getParabolaGweeps () - 1);
			}
				
			playerController.setCurrentGweep("null");
			GweepImage.sprite = null;
		}
	}

    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        m_Anim.SetBool("Ground", m_Grounded);

        // Set the vertical animation
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch && m_Anim.GetBool("Crouch"))
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        // Set whether or not the character is crouching in the animator
        m_Anim.SetBool("Crouch", crouch);

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Reduce the speed if crouching by the crouchSpeed multiplier
            move = (crouch ? move*m_CrouchSpeed : move);

            // The Speed animator parameter is set to the absolute value of the horizontal input.
            m_Anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
                // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump && m_Anim.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

public bool facingRight(){
	return m_FacingRight;
}	

}
