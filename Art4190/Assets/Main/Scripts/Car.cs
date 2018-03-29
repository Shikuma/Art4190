using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	public Vector3 nextDest, deSpawnLoc, tempStopLoc;

	public bool stopped, isDeparting;
	public int lane;
	private GameObject gc;
	TrafficController tc;

	// Use this for initialization
	void Start () {
		isDeparting = false;
		gc = GameObject.FindWithTag("GameController");
		tc = gc.GetComponent<TrafficController>();
	}

	// Update is called once per frame
	void Update() {
		if (isDeparting) {
			transform.position = Vector3.MoveTowards(transform.position, deSpawnLoc, .1f);	
		}else if (!stopped) { 
			//transform.Translate((transform.position - nextDest).normalized);
			transform.position = Vector3.MoveTowards(transform.position, tempStopLoc, .1f);
			if(Vector3.Distance(tempStopLoc, transform.position) <= 0.1f) {
				StartCoroutine(TempStop());
			}
			if(Vector3.Distance(nextDest, transform.position) <= 0.1f) {
				//StartCoroutine(TempStop());
				if(!isDeparting)tc.stopQ.Add(gameObject);
			}
		}
		if(tc.stopQ.Count > 0 && tc.stopQ[0] == gameObject) {
			print("Stopping?");
			StartCoroutine(Stop());
		}		
	}

	private IEnumerator TempStop(){
		yield return new WaitForSeconds(0.25f);
		stopped = true;
	}

	private IEnumerator Stop() {
		yield return new WaitForSeconds(2f);
		stopped = false;
		tc.stopQ.Remove(gameObject);
		tc.lanes[lane].Remove(gameObject);
		// upodatygdsvmdjh . lane
		foreach (GameObject item in tc.lanes[lane]) {
			Car car = item.GetComponent<Car>();
			Vector3 stopDest = car.tempStopLoc;
			int index = tc.lanes[lane].IndexOf(item);

		//tc.lanes[lane][index-1].transform.position;
		switch (car.lane) {
			case 0:
				stopDest.x += 3f;
				break;
			case 1:
				stopDest.z += 3f;
				break;
			case 2:
				stopDest.z -= 3f;
				break;
			case 3:
				stopDest.x -= 3f;
				break;
			default:
				break;
			}
			car.tempStopLoc = stopDest;
			car.stopped = false;
		}
		nextDest = deSpawnLoc;
		isDeparting = true;
	}
}
