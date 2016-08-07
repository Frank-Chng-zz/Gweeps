using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour {
	private GameObject RestartButton;
	private GameObject ReturnButton;
	private GameObject ExitButton;

	public void DisplayMenu(){
		RestartButton = gameObject.transform.Find ("Restart").gameObject;
		ReturnButton = gameObject.transform.Find ("Return").gameObject;
		ExitButton = gameObject.transform.Find ("Exit").gameObject;

		RestartButton.SetActive (true);
		ReturnButton.SetActive (true);
		ExitButton.SetActive (true);
		Time.timeScale = 0f;

	}




}
