using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour {

	public GameObject[] spawnLocs, deSpawnLocs, stopLocs;
	public GameObject carPrefab, carParent;
	public float spawnInterval;



	// Use this for initialization
	void Start () {
		spawnInterval = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator SpawnCar() {
		yield return new WaitForSeconds(spawnInterval);

		GameObject currCar = Instantiate(carPrefab, spawnLocs[Random.Range(0, 3)].transform.position, Quaternion.identity, carParent.transform);
		//Set rotation of car

		//Add to queue

		//Set stop location

	}
}
