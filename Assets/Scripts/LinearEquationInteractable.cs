using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LinearEquationInteractable : MonoBehaviour {

	private Slider slider;
	private Text adjustableNumber;
	private float slopeValue;

	//This is being re-enabled every single frame, because of DisplayEquation code.
	void OnEnable(){
		SliderInteraction ();
	}

	// Use this for initialization
	void SliderInteraction () {
		slider = transform.Find ("Slider").gameObject.GetComponent<Slider> ();
		adjustableNumber = transform.Find ("AdjustableNumber").gameObject.GetComponent<Text> ();
		//so the value of the slope can go between 0.25 and 1.25
		slopeValue = slider.value + 0.25f; 
		adjustableNumber.text = slopeValue.ToString("0.00");
	}





	public float getSlopeValue(){
		return slopeValue;
	}


}
