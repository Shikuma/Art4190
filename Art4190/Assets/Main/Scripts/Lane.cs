using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour {

	public GameObject[] cars;
	public int currChildToSpawn, maxChildren, currChildCount, laneNum, carsInLane;
	public bool full;

	public Lane(int maxChildren, int laneNum) {
		cars = new GameObject[maxChildren];
		this.maxChildren = maxChildren;
		this.laneNum = laneNum;
		full = false;
	}
}
