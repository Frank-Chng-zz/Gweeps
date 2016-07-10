using UnityEngine;
using System.Collections;

public class ChangeTextLevel01 : MonoBehaviour {

	public GameObject key;
	private GUIText level1text;

	void Start(){
		level1text = GetComponent<GUIText> ();
	}

	// Update is called once per frame
	void Update () {
		if (!key.activeSelf) {
			level1text.text = "Good job! Now go to the door!";
		}
	}
}
