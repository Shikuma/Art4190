using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonController : MonoBehaviour {
	public GameObject goalObject, gameController, goodPS, badPS;

	private Vector3 goalLocation;
	private NavMeshAgent agent;

	GameStats gs;
	Dialogue diag;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		gameController = GameObject.FindWithTag("GameController");
		gs = gameController.GetComponent<GameStats>();
		diag = gameController.GetComponent<Dialogue>();
	}
	
	// Update is called once per frame
	void Update () {
		if(goalLocation != goalObject.transform.position) {
			goalLocation = goalObject.transform.position;
		}
		agent.destination = goalLocation;

		if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)) {
			Die(15f, 3);
			if(Random.Range(0,10) >= 7)
				diag.GiveGoodFeedBack();
		}
	}

	private void OnTriggerEnter(Collider c) {
		if(c.gameObject.tag == "car") {
			print("DEAD ========================");
			Die(-15f, -15);
			diag.GiveBadFeedBack();
		}
	}

	void Die(float score, int rating) {
		gs.UpdateMultiplier(gs.UpdateScore(score) > 0);
		gs.UpdateRating_Pedestrian(rating);
		GameObject go;
		if (score > 0) {
			go = Instantiate(goodPS, transform.position, Quaternion.identity);
		}
		else {
			go = Instantiate(badPS, transform.position, Quaternion.identity);
		}
		Destroy(go, 3f);
		Destroy(gameObject);
	}
	
}
