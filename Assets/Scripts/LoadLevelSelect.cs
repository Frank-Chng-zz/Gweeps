using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelSelect : MonoBehaviour {


	public void GoToLevelSelect(){
		Time.timeScale = 1f;
		SceneManager.LoadScene(1);
	}


}
