using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelSelect : MonoBehaviour {


	public void GoToLevelSelect(){
		SceneManager.LoadScene(1);
	}


}
