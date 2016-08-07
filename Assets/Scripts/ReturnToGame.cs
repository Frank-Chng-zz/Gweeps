using UnityEngine;
using System.Collections;

public class ReturnToGame : MonoBehaviour {
	private GameObject RestartButton;
	private GameObject ReturnButton;
	private GameObject ExitButton;

	public void CloseMenu(){
		Time.timeScale = 1f;
		RestartButton = GameObject.FindGameObjectWithTag ("HUDCanvas").transform.Find ("Menu").Find ("Restart").gameObject;
		ReturnButton = GameObject.FindGameObjectWithTag ("HUDCanvas").transform.Find ("Menu").Find ("Return").gameObject;
		ExitButton = GameObject.FindGameObjectWithTag ("HUDCanvas").transform.Find ("Menu").Find ("Exit").gameObject;

		RestartButton.SetActive (false);
		ReturnButton.SetActive (false);
		ExitButton.SetActive (false);

	}	

}
