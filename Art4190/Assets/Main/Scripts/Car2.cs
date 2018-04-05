using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car2 : MonoBehaviour {

	NavMeshAgent agent;

	public float stopDuration;
	public bool stopped, departed;
	public Vector3 stopLocation, nextDestination, currDestination, despawnLocation;
	public int lane, currStopQ;

	GameObject gc;
	TrafficController tc;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		gc = GameObject.FindWithTag("GameController");
		tc = gc.GetComponent<TrafficController>();
		currDestination = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (!departed) {
			if (currDestination != nextDestination) {
				currDestination = nextDestination;
				agent.destination = currDestination;
				Debug.Log("Stop dest: " + stopLocation + ". Curr dest: " + currDestination);
			}
			if (transform.position.x == stopLocation.x && transform.position.z == stopLocation.z && !stopped) {
				stopped = true;
				tc.stopQ.Add(gameObject);
				currStopQ = tc.stopQ.Count - 1;
				Debug.Log("Car is stopped in position: " + currStopQ + ".");
			}
			else if (stopped && currStopQ == 0) {
				StartCoroutine(Stop());
			}
		}
		
	}

	private IEnumerator Stop() {
		yield return new WaitForSeconds(stopDuration);
		departed = true;
		stopped = false;
		agent.destination = despawnLocation;

		//Update the Stop Queue
		tc.stopQ.RemoveAt(0);
		//Move all cars in the StopQ up 1 position in the queue
		//This would be so much easier if queues worked kms
		if (tc.stopQ.Count > 1) {
			for (int i = 1; i < tc.stopQ.Count; i++) {
				tc.stopQ[i].GetComponent<Car2>().currStopQ--;
				tc.stopQ[i - 1] = tc.stopQ[i];
				tc.stopQ.RemoveAt(i);
			}
		}

		//Update the lane list
		//tempLane.Add(gameObject);
		List<GameObject> tempLane = new List<GameObject>();
		tempLane.AddRange(tc.lanes[lane]);
		//Debug.Log("STOPPED COUNT: " + tempLane);
		Debug.Log("STOPPED LANE: " + lane);
		tempLane.RemoveAt(0);
		//Move all cars in the lane up 1 space
		if (tempLane.Count > 1) {
			for (int i = 1; i < tempLane.Count; i++) {
				Car2 car = tempLane[i].GetComponent<Car2>();
				switch (lane) {
					case 0:
						car.nextDestination.x += 3f;
						break;
					case 1:
						car.nextDestination.z += 3f;
						break;
					case 2:
						car.nextDestination.z -= 3f;
						break;
					case 3:
						car.nextDestination.x -= 3f;
						break;
					default:
						break;

				}
				tempLane[i - 1] = tempLane[i];
				tempLane.RemoveAt(i);
			}
		}
	}
}
