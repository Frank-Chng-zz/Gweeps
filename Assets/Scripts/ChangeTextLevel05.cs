using UnityEngine;
using System.Collections;

public class ChangeTextLevel05 : MonoBehaviour {

	private GUIText text;
	private int count = 0;

	void Start(){
		text = GetComponentInParent<GUIText> ();
	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player") && count == 0){
			text.text = "This is <insert name here> the line Gweep. \n He bounces off things. You can adjust the slope of his movement! \n Fire him in a direction and see how he moves. \n Try to knock the box onto the button! \n (hint) step on the box and set slope to 0.5";
			count = count + 1;
		}

	}
}
