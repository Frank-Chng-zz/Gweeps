using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Player2DMovement : MonoBehaviour
    {
		
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
		private LeftScript LS;
		private RightScript RS;



        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
			LS = GameObject.FindGameObjectWithTag ("HUDCanvas").transform.Find ("Controls").transform.Find ("Left").gameObject.GetComponent<LeftScript> ();
			RS = GameObject.FindGameObjectWithTag ("HUDCanvas").transform.Find ("Controls").transform.Find ("Right").gameObject.GetComponent<RightScript> ();

        }



		public void setJump(){
			m_Jump = true;
		}

        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
			float h;
			if (RS.GetRight ()) {
				h = 1.0f;
				LS.SetLeft ();
			}
			else if (LS.GetLeft ()) {
				h = -1.0f;
				RS.SetRight ();
			}   else {
				h = 0f;
				LS.SetLeft ();
				RS.SetRight ();
			}
			//float h = CrossPlatformInputManager.GetAxis("Horizontal");

            // Pass all parameters to the character control script.
            
			m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    	

	}
}
