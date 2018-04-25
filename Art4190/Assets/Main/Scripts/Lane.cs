using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour {

	public List<GameObject> cars;
	public int currChildToSpawn, maxChildren, currChildCount, laneNum, carsInLane;
	public bool full;

	public Lane(int maxChildren, int laneNum) {
		cars = new List<GameObject>();
		this.maxChildren = maxChildren;
		this.laneNum = laneNum;
		full = false;
	}
}
