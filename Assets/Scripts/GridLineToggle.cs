using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridLineToggle : MonoBehaviour, IPointerClickHandler {

	private bool GridLines = false;
	private DrawGridLines DGL;

	 void Start(){
		DGL = GameObject.FindGameObjectWithTag ("Boundary").GetComponent<DrawGridLines> ();
	}

	public void OnPointerClick(PointerEventData eventData){
		if (GridLines == true) {
			GridLines = false;
			gameObject.GetComponentInChildren<Text>().text = "Grid Lines: Off";
			DGL.DeleteLines();
		} else {
			GridLines = true;
			gameObject.GetComponentInChildren<Text>().text = "Grid Lines: On";
			DGL.DrawLines();
		}
			
	}	
}