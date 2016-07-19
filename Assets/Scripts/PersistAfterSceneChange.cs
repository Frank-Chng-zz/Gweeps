using UnityEngine;
using System.Collections;

public class PersistAfterSceneChange : MonoBehaviour {

	private static PersistAfterSceneChange instance = null;
	public static PersistAfterSceneChange Instance{
		get{return instance;}
	}

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
