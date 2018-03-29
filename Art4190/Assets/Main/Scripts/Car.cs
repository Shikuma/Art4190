using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	public Vector3 nextDest;
	public bool stopped;
	public int lane;

	private GameObject gc;
	TrafficController tc;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindWithTag("GameController");
		tc = gc.GetComponent<TrafficController>();
	}

	// Update is called once per frame
	void Update() {
		if (!stopped) { 
			//transform.Translate((transform.position - nextDest).normalized);
			transform.position = Vector3.MoveTowards(transform.position, nextDest, .1f);
			if(Vector3.Distance(nextDest, transform.position) <= 0.1f) {
				stopped = true;
				tc.stopQ.Enqueue(gameObject);
			}
		}
		else {
			if(tc.stopQ.Count > 0 && tc.stopQ.Peek() == gameObject) {
				StartCoroutine(Stop());
			}
		}

		
		
	}

	private IEnumerator Stop() {
		yield return new WaitForSeconds(3f);
		if (tc.stopQ.Count > 0) {
			tc.stopQ.Dequeue();
			tc.lanes[lane].Dequeue();
		}
	}
}
