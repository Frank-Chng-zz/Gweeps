using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public void StartTheGame(){
		Time.timeScale = 1f;
		SceneManager.LoadScene (2);
	}
}
