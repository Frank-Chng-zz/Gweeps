using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityStandardAssets._2D;


public class RightScript : MonoBehaviour {

	private bool right = false;
	//private bool onRight = false;
	private GraphicRaycaster GR;
	private LeftScript LS;
	//private Player2DMovement playerMovement;


	void Start(){
		GR = GameObject.FindGameObjectWithTag ("HUDCanvas").GetComponent<GraphicRaycaster> ();
		LS = GameObject.FindGameObjectWithTag ("HUDCanvas").transform.Find ("Controls").transform.Find ("Left").gameObject.GetComponent<LeftScript> ();
		//playerMovement = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player2DMovement> ();

	}

	void Update(){
		if (Input.touchCount > 0) {
			//Start touching
			//if (Input.GetTouch (0).phase == TouchPhase.Began) {
				
			//}

			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				//if (onRight) {
				PointerEventData ped = new PointerEventData (null);
				ped.position = Input.GetTouch (0).position;
				List<RaycastResult> hits = new List<RaycastResult> ();
				GR.Raycast(ped, hits);

				if (hits != null) {
					foreach (RaycastResult hit in hits) {

						print (hit.gameObject.name);
						if (hit.gameObject.name == "Right") {
							right = true;
							//onRight = true;
							LS.SetLeft ();
						}
					}

				}
				//}

			}

			if (Input.GetTouch (0).phase == TouchPhase.Ended) {
				//if (onRight) {
					right = false;
				//}

			}
		}
	}

	public void SetRight(){
		right = false;
	}

	public bool GetRight(){
		return right;
	}

}
