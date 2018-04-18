using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour {

	[SerializeField]
	float score, money, rating, multiplier;
	[SerializeField]
	int deaths;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float UpdateScore(float n) {
		score += n*multiplier;
		if (n < 0) score = 0;
		return score;
	}

	public void UpdateMultiplier(bool shouldReset) {
		multiplier = shouldReset ? 0 : multiplier + 1;
	}
}
