using UnityEngine;
using System.Collections;

public class ChangeTextLevel07 : MonoBehaviour {

	private GUIText text;
	private int count = 0;

	void Start(){
		text = GetComponentInParent<GUIText> ();
	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player") && count == 0){
			text.text = "This is <insert name here> the parabola Gweep. \n Adjust his trajectory with the sliders, and shoot. \n Hit the button!";
			count = count + 1;
		}

	}
}
