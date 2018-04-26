using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausing : MonoBehaviour {

	[SerializeField]
	bool paused;

	public GameObject pausePanel, howToPlayPanel;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Pause() {
		paused = !paused;
		Time.timeScale = paused ? 0 : 1;
	}

	public void PauseMidGame() {
		Pause();
		pausePanel.SetActive(paused);
	}

	public void PauseEndGame() {
		Pause();
	}

	public void HowToPlay() {
		pausePanel.SetActive(!pausePanel.activeSelf);
		howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
	}
}
