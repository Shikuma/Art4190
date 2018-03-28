using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	public Vector3 nextDest;
	public bool stopped;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!stopped)
			transform.Translate((transform.position - nextDest).normalized);
		
	}
}
