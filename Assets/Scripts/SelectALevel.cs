using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectALevel : MonoBehaviour {
	
	public void SelectLevel(int level){
		Time.timeScale = 1f;
		SceneManager.LoadScene (level + 1);
	}


}
