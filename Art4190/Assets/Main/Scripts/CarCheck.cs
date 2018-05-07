using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheck : MonoBehaviour {

	GameStats gs;
	GameObject gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindGameObjectWithTag("GameController");
		gs = gc.GetComponent<GameStats>();
	}

	private void OnTriggerEnter(Collider c) {
		if(c.tag == "car") {
			print("=== HIT ANOTHER CAR ===");
			Car2 otherCar = c.GetComponent<Car2>();
			Car2 thisCar = GetComponentInParent<Car2>();
			if (otherCar.lane != thisCar.lane) {
				gs.UpdateRating_Driver(-15);
			}
		}
	}
}
