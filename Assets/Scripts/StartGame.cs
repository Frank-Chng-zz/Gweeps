using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public void StartTheGame(){
		SceneManager.LoadScene (2);
	}
}
