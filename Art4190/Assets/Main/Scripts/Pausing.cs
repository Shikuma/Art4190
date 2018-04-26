using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausing : MonoBehaviour {

	[SerializeField]
	bool paused;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pause() {
		paused = !paused;
		Time.timeScale = paused ? 0 : 1;
	}
}
