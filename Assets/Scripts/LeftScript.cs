using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityStandardAssets._2D;


public class LeftScript : MonoBehaviour {
	private bool left = false;
	//private bool onLeft = false;
	private GraphicRaycaster GR;
	private RightScript RS;
	//private Player2DMovement playerMovement;

	void Start(){
		GR = GameObject.FindGameObjectWithTag ("HUDCanvas").GetComponent<GraphicRaycaster> ();
		RS = GameObject.FindGameObjectWithTag ("HUDCanvas").transform.Find ("Controls").transform.Find ("Right").gameObject.GetComponent<RightScript> ();
		//playerMovement = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player2DMovement> ();
	}

	void Update(){
		if (Input.touchCount > 0) {
			//Start touching
			//if (Input.GetTouch (0).phase == TouchPhase.Began) {
				
			//}

			if (Input.GetTouch (0).phase == TouchPhase.Stationary) {
				//if (onLeft) {
				PointerEventData ped = new PointerEventData (null);
				ped.position = Input.GetTouch (0).position;
				List<RaycastResult> hits = new List<RaycastResult> ();
				GR.Raycast(ped, hits);

				if (hits != null) {
					foreach (RaycastResult hit in hits) {

						print (hit.gameObject.name);
						if (hit.gameObject.name == "Left") {
							left = true;
							//onLeft = true;
							RS.SetRight ();
						}
					}

				}
				//}
					
			}


			if (Input.GetTouch (0).phase == TouchPhase.Ended) {
				//if (onLeft) {
					left = false;
					print ("Left off!");
				//}

			}
		}
	}

	public void SetLeft(){
		left = false;
	}

	public bool GetLeft(){
		return left;
	}
}
