using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragButtonLGSlider : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerClickHandler {

	private ProjectilePreview PP;
	private PlatformerCharacter2D PC2D;

	void OnEnable () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		PP = player.GetComponent<ProjectilePreview> ();
		PC2D = player.GetComponent<PlatformerCharacter2D> ();
	}
		
	public void OnDrag(PointerEventData eventData){
		PP.DeleteDots ();
		if (PC2D.facingRight ()) {
			PP.UpdateLinTrajectory ("Right");
		} else {
			PP.UpdateLinTrajectory ("Left");
		}

	}

	public void OnPointerDown(PointerEventData eventData){
		PP.DeleteDots ();
		if (PC2D.facingRight ()) {
			PP.UpdateLinTrajectory ("Right");
		} else {
			PP.UpdateLinTrajectory ("Left");
		}
	}

	public void OnPointerClick(PointerEventData eventData){
		PP.DeleteDots ();
		if (PC2D.facingRight ()) {
			PP.UpdateLinTrajectory ("Right");
		} else {
			PP.UpdateLinTrajectory ("Left");
		}
	}


}
