using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car2 : MonoBehaviour {

	NavMeshAgent agent;

	public float stopDuration;
	public bool stopped, departed;
	public Vector3 stopLocation, nextDestination, currDestination, despawnLocation;
	public int lane, currStopQ, carID;

	GameObject gc;
	TrafficController2 tc;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		gc = GameObject.FindWithTag("GameController");
		tc = gc.GetComponent<TrafficController2>();
		currDestination = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (!departed) {
			if (currDestination != nextDestination) {
				currDestination = nextDestination;
				agent.destination = currDestination;
				Debug.Log("ID: " + lane + "-" + carID + ". Stop dest: " + stopLocation + ". Curr dest: " + currDestination);
			}
			if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) && currDestination == stopLocation && !stopped) {
				//print("ID: " + lane + "-" + carID + ". STOPPED IN STOPQ");
				stopped = true;
				tc.stopQ[tc.carsInStopQ] = gameObject;
				tc.carsInStopQ++;
				currStopQ = tc.carsInStopQ;
			}
			else if (stopped && currStopQ == 1 && tc.carsInStopQ > 0) {
				StartCoroutine(Stop());
			}
		}
		else if (departed && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) && currDestination == despawnLocation) {
			Destroy(gameObject);
		}
	}

	private IEnumerator Stop() {
		departed = true;
		stopped = false;
		//print("ID: " + lane + "-" + carID + ". DEPARTING IN LANE: " + lane);
		print(tc.stops[lane] + "==========================");
		yield return new WaitForSeconds(tc.stops[lane] ? stopDuration : 0f);
		tc.lanes[lane].carsInLane = tc.lanes[lane].carsInLane > 0 ? tc.lanes[lane].carsInLane-1 : 0;
		//tc.lanes[lane].carsInLane--;
		currDestination = despawnLocation;
		agent.destination = despawnLocation;

		//Update the Stop Queue
		//Move all cars in the StopQ up 1 position in the queue
		tc.carsInStopQ = tc.carsInStopQ > 0 ? tc.carsInStopQ - 1 : tc.carsInStopQ = 0;
		if (tc.carsInStopQ > 0) {
			for (int i = 1; i < tc.carsInStopQ+1; i++) {
				tc.stopQ[i].GetComponent<Car2>().currStopQ--;
				tc.stopQ[i - 1] = tc.stopQ[i];
			}
		}

		//print("Attempting to update the rest of the lane. Cars in lane: " + tc.lanes[lane].carsInLane + ". ");
		if (tc.lanes[lane].carsInLane > 0) {
			int currIndex = carID;
			//Loop the cars that are currently in the lane to move up one space
			for (int i = 0; i < tc.lanes[lane].carsInLane; i++) {
				//Wait half a second to let the car in front move.
				yield return new WaitForSeconds(1f);
				
				currIndex++;
				if (currIndex > tc.lanes[lane].maxChildren-1) currIndex = 0;
				//print("ID: " + lane + "-" + carID + ". Updating car at index: " + currIndex + ".");
				Car2 car = tc.lanes[lane].cars[currIndex].GetComponent<Car2>();
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
			}
		}

		/*
		print("EG: " + tc.lanes.Length);
		//Update the lane list
		//tempLane.Add(gameObject);
		//List<GameObject> tempLane = new List<GameObject>();
		//tempLane.AddRange(tc.lanes[lane]);
		//Debug.Log("STOPPED COUNT: " + tempLane);
		Debug.Log("STOPPED LANE: " + lane);
		tc.lanes[lane].RemoveAt(0);
		//Move all cars in the lane up 1 space
		if (tc.lanes[lane].Count > 1) {
			for (int i = 1; i < tc.lanes[lane].Count; i++) {
				Car2 car = tc.lanes[lane][i].GetComponent<Car2>();
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
				tc.lanes[lane][i - 1] = tc.lanes[lane][i];
				tc.lanes[lane].RemoveAt(i);
				print("Previous car dest: " + tc.lanes[lane][i - 1].gameObject.GetComponent<Car2>().nextDestination);
				//tc.lanes[lane].AddRange(tc.lanes[lane]);
			}
		}
		*/
	}
}
