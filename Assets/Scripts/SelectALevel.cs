using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectALevel : MonoBehaviour {
	
	public void SelectLevel(int level){
		SceneManager.LoadScene (level + 1);
	}


}
