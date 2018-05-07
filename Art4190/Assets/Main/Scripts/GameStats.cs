using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

	[SerializeField]
	public float score, money, rating_pedestrian, rating_driver, multiplier;
	[SerializeField]
	int deaths;

	public Text scoreText;
	public Slider happiness_pedestrian, happiness_driver;

	// Use this for initialization
	void Start () {
		scoreText.text = "" +  score;
		happiness_pedestrian.value = happiness_pedestrian.maxValue;
		rating_pedestrian = happiness_pedestrian.maxValue;
		rating_driver = happiness_driver.maxValue;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float UpdateScore(float n) {
		score += n*multiplier;
		// if (n < 0) score = 0;
		print("hi");
		scoreText.text = "" + score;
		return score;
	}

	public void UpdateMultiplier(bool shouldReset) {
		multiplier = shouldReset ? 1 : multiplier + 1;
	}

	public void UpdateRating_Pedestrian(int n) {
		rating_pedestrian += n;
		if (rating_pedestrian > 100) rating_pedestrian = 100;
		else if (rating_pedestrian < 0) rating_pedestrian = 0;
		print("rating_pedestrian " + rating_pedestrian);
		happiness_pedestrian.value = rating_pedestrian;
		//happinessPercent.text = "" + ((rating_pedestrian / happiness_pedestrian.maxValue) * 100) + "%";
	}

	public void UpdateRating_Driver(int n) {
		rating_driver += n;
		if (rating_driver > 100) rating_driver = 100;
		else if (rating_driver < 0) rating_pedestrian = 0;
		happiness_driver.value = rating_driver;
	}
	
}
