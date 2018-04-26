using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pausing : MonoBehaviour {

	public bool paused;

	public GameObject pausePanel, howToPlayPanel, endGamePanel;
	public Text ratingText;
	
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

	public void PauseMidGame() {
		Pause();
		pausePanel.SetActive(paused);
	}

	public void PauseEndGame(int rating) {
		Pause();
		endGamePanel.SetActive(!endGamePanel.activeSelf);
		if (paused){
			if (rating < 25) ratingText.text = "You did poorly :(";
			else if (rating < 50) ratingText.text = "You did okay..";
			else if (rating < 75) ratingText.text = "You did pretty good!";
			else ratingText.text = "You did amazing! :)";
		}
	}

	public void HowToPlay() {
		pausePanel.SetActive(!pausePanel.activeSelf);
		howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
	}
}
