using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

	[SerializeField]
	public float score, money, rating, multiplier;
	[SerializeField]
	int deaths;

	public Text scoreText, happinessPercent;
	public Slider happinessMeter;

	// Use this for initialization
	void Start () {
		scoreText.text = "" +  score;
		happinessMeter.value = happinessMeter.maxValue;
		rating = happinessMeter.maxValue;
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

	public void UpdateRating(int n) {
		rating += n;
		if (rating > 100) rating = 100;
		else if (rating < 0) rating = 0;
		print("RATING " + rating);
		happinessMeter.value = rating;
		//happinessPercent.text = "" + ((rating / happinessMeter.maxValue) * 100) + "%";
	}

	public int Rating() {
		return (int)rating;
	}
}
