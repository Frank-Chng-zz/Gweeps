using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

	public void Restart(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}


}
