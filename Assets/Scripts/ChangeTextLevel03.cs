using UnityEngine;
using System.Collections;

public class ChangeTextLevel03 : MonoBehaviour {

	private GUIText text;
	private int count = 0;
	void Start(){
		text = GetComponentInParent<GUIText> ();
	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player") && count == 0){
			text.text = "This is <insert name here> the Constant Gweep.";
			count = count + 1;
		}

	}
}
