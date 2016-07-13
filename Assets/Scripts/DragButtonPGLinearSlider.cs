using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragButtonPGLinearSlider : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerClickHandler{
	
	private ProjectilePreview PP;
	private PlatformerCharacter2D PC2D;
	// Use this for initialization
	void OnEnable () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		PP = player.GetComponent<ProjectilePreview> ();
		PC2D = player.GetComponent<PlatformerCharacter2D> ();
	}
	
	public void OnDrag(PointerEventData eventData){
		PP.DeleteDots ();
		if (PC2D.facingRight ()) {
			PP.UpdateQuadraticTrajectory ("Right");
		} else {
			PP.UpdateQuadraticTrajectory ("Left");
		}

	}

	public void OnPointerDown(PointerEventData eventData){
		PP.DeleteDots ();
		if (PC2D.facingRight ()) {
			PP.UpdateQuadraticTrajectory ("Right");
		} else {
			PP.UpdateQuadraticTrajectory ("Left");
		}
	}

	public void OnPointerClick(PointerEventData eventData){
		PP.DeleteDots ();
		if (PC2D.facingRight ()) {
			PP.UpdateQuadraticTrajectory ("Right");
		} else {
			PP.UpdateQuadraticTrajectory ("Left");
		}
	}
}
