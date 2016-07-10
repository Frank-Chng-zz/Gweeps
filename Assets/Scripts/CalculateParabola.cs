using UnityEngine;
using System.Collections;
using System;
public class CalculateParabola : MonoBehaviour {

	public float calculateMax(float linearCoef, float quadraticCoef){
		float constant = 10f;
		float firstTerm = (linearCoef * linearCoef) / 4;
		float secondTerm = (-linearCoef * linearCoef) / (2 * quadraticCoef);
		return firstTerm + secondTerm + constant;

	}

	public float calculateLeftRoot(float linearCoef, float quadraticCoef){
		float discriminant = (float)(Math.Sqrt ((double)((linearCoef * linearCoef) - (40 * quadraticCoef))));
		float numerator = -linearCoef - discriminant;
		return (numerator / (2 * quadraticCoef));
	}

	public float calculateRightRoot(float linearCoef, float quadraticCoef){
		float discriminant = (float)(Math.Sqrt ((double)((linearCoef * linearCoef) - (40 * quadraticCoef))));
		float numerator = -linearCoef + discriminant;
		return (numerator / (2 * quadraticCoef));
	}
}
