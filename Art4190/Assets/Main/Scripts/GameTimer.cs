using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	public float timeLeft;
	public int hours;

	Pausing pausing;

	public Text timerText;

	// Use this for initialization
	void Start () {
		pausing = GetComponent<Pausing>();
		hours = (int)(((timeLeft % 60)/60)*24);
		timerText.text = "" + (int)(timeLeft / 60) + " Days and " + hours + " hours";
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		hours = (int)(((timeLeft % 60) / 60) * 24);
		timerText.text = "" + (int)(timeLeft / 60) + " Days and " + hours + " hours";
		if (timeLeft < 0) {
			GameOver();
		}
	}

	public void GameOver() {
		pausing.PauseEndGame();
		Debug.Log("Game Has Ended");
	}
}
