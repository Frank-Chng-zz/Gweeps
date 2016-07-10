using UnityEngine;
using System.Collections;

public class ChangeText_2_Level03 : MonoBehaviour {

	private GUIText text;
	private int count = 0;

	void Start(){
		text = GetComponentInParent<GUIText> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player") && count == 0){
			text.text = "       Press space to fire \nCarl at the bricks to destroy them! \n He moves in a horizontal line. \n Press r if you need to restart. \n Carl also respawns if you miss!";
			count = count + 1;
		}


	}
}
