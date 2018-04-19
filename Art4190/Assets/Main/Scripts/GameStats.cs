using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

	[SerializeField]
	float score, money, rating, multiplier;
	[SerializeField]
	int deaths;

	public Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText.text = "" +  score;
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
}
