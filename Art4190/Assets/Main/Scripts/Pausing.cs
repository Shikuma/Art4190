using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pausing : MonoBehaviour {

	public bool paused;

	public GameObject pausePanel, howToPlayPanel, endGamePanel;
	public Text rating_pedestrianText;
	
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

	public void PauseEndGame(int rating_pedestrian, int rating_driver) {
		Pause();
		endGamePanel.SetActive(!endGamePanel.activeSelf);
		if (paused){
			if (rating_pedestrian < 25) rating_pedestrianText.text = "The pedestrians were not safe enough, ";
			else if (rating_pedestrian < 50) rating_pedestrianText.text = "The pedestrians were scared for their lives, ";
			else if (rating_pedestrian < 75) rating_pedestrianText.text = "The pedestrians were moderately happy, ";
			else rating_pedestrianText.text = "The pedestrians were very happy, ";

			if (rating_driver < 25) rating_pedestrianText.text += " and the drivers were not safe enough.";
			else if (rating_driver < 50) rating_pedestrianText.text += " and the drivers were not very happy.";
			else if (rating_driver < 75) rating_pedestrianText.text += " and the drivers were moderately happy!";
			else rating_pedestrianText.text += " and the drivers were very happy!";
		}
	}

	public void HowToPlay() {
		pausePanel.SetActive(!pausePanel.activeSelf);
		howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
	}
}
