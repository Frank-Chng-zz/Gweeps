using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuadraticEquationInteractable : MonoBehaviour {

	private Slider linearSlider;
	private Slider quadraticSlider;
	private Text linearCoef;
	private Text quadraticCoef;
	private float linearValue;
	private float quadraticValue;

	//This is being re-enabled every single frame, because of DisplayEquation code.
	void OnEnable(){
		SliderInteraction ();
	}

	// Use this for initialization
	void SliderInteraction () {
		linearSlider = transform.Find ("Linear Part").gameObject.transform.Find("linearSlider").gameObject.GetComponent<Slider> ();
		quadraticSlider = transform.Find ("Quadratic Part").gameObject.transform.Find("quadraticSlider").gameObject.GetComponent<Slider> ();
		linearCoef = transform.Find ("Linear Part").gameObject.transform.Find("linearCoef").gameObject.GetComponent<Text> ();
		quadraticCoef = transform.Find ("Quadratic Part").gameObject.transform.Find("quadraticCoef").gameObject.GetComponent<Text> ();
		//so the value of the slope can go between 0.25 and 1.25
		linearValue = linearSlider.value;
		quadraticValue = quadraticSlider.value + 0.5f;
		linearCoef.text = linearValue.ToString("0.00");
		quadraticCoef.text = quadraticValue.ToString ("0.00");
	}


	public float getLinearValue(){
		return linearValue;
	}

	public float getQuadraticValue(){
		return quadraticValue;
	}

}
