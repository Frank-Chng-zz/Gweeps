using UnityEngine;
using System.Collections;
using System;

public class CalculateParabola : MonoBehaviour {

	public QuadraticEquationInteractable QEI;

	public float calculateMax(float linearCoef, float quadraticCoef){
		float constant = 10f;
		float firstTerm = (-linearCoef * linearCoef) / (4 * quadraticCoef);
		float number = firstTerm + constant;
		return number;

	}

	public float calculateLeftRoot(float linearCoef, float quadraticCoef){
		float discriminant = (float)(Math.Sqrt ((double)((linearCoef * linearCoef) - (40 * quadraticCoef))));
		float numerator = -(linearCoef - discriminant);
		float number = (numerator / (2 * quadraticCoef));
		return number;
	}

	public float calculateRightRoot(float linearCoef, float quadraticCoef){
		float discriminant = (float)(Math.Sqrt ((double)((linearCoef * linearCoef) - (40 * quadraticCoef))));
		float numerator = -(linearCoef + discriminant);
		float number = (numerator / (2 * quadraticCoef));
		return number;
	}

	public float quadraticFunction(float xValue){
		float linearCoef = QEI.getLinearValue ();
		float quadraticCoef = QEI.getQuadraticValue ();
		return (quadraticCoef * xValue * xValue) + (linearCoef * xValue) + 10;
	}

}
