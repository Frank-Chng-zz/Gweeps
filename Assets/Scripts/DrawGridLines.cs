using UnityEngine;
using System.Collections;

public class DrawGridLines : MonoBehaviour {

	private int horizontalSize;
	private int verticalSize;
	public GameObject lineDrawer; 
	private float min_X;
	private float max_X;
	private float min_Y;
	private float max_Y;

	// Use this for initialization
	void Start () {
		horizontalSize = (int)gameObject.transform.lossyScale.x;
		verticalSize = (int)gameObject.transform.lossyScale.y;
		min_X = gameObject.transform.position.x - (float)horizontalSize/2;
		max_X = gameObject.transform.position.x + (float)horizontalSize/2;
		min_Y = gameObject.transform.position.y - (float)verticalSize/2;
		max_Y = gameObject.transform.position.y + (float)verticalSize/2;
		DrawLines ();

		if (gameObject.transform.position.z != 1) {
			gameObject.transform.position = gameObject.transform.position + new Vector3 (0, 0, 1);
		}

	}


	void DrawLines(){
		//Drawing horizontal lines
		for(int i = 0; i < verticalSize + 1; i++){
			GameObject drawer = Instantiate(lineDrawer) as GameObject;
			drawer.transform.parent = gameObject.transform;
			LineRenderer renderer = drawer.GetComponent<LineRenderer> ();
			renderer.SetVertexCount (2);
			renderer.SetWidth (0.03f, 0.03f);
			renderer.SetPosition (0, new Vector3 (min_X, min_Y + i, 1));
			renderer.SetPosition (1, new Vector3 (max_X, min_Y + i, 1));
		}

		for (int i = 0; i < horizontalSize + 1; i++) {
			GameObject drawer = Instantiate(lineDrawer) as GameObject;
			drawer.transform.parent = gameObject.transform;
			LineRenderer renderer = drawer.GetComponent<LineRenderer> ();
			renderer.SetVertexCount (2);
			renderer.SetWidth (0.03f, 0.03f);
			renderer.SetPosition (0, new Vector3 (min_X + i, min_Y, 1));
			renderer.SetPosition (1, new Vector3 (min_X + i, max_Y, 1));
		}

	}
}
