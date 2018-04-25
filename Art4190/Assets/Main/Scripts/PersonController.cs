using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonController : MonoBehaviour {
	public GameObject goalObject, gameController;

	private Vector3 goalLocation;
	private NavMeshAgent agent;

	GameStats gs;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		gameController = GameObject.FindWithTag("GameController");
		gs = gameController.GetComponent<GameStats>();
	}
	
	// Update is called once per frame
	void Update () {
		if(goalLocation != goalObject.transform.position) {
			goalLocation = goalObject.transform.position;
		}
		agent.destination = goalLocation;

		if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)) {
			Die(25f);
		}
	}

	private void OnTriggerEnter(Collider c) {
		if(c.gameObject.tag == "car") {
			Die(-10f);
		}
	}

	void Die(float n) {
		gs.UpdateMultiplier(gs.UpdateScore(n) > 0);
		Destroy(gameObject);
	}
	
}
