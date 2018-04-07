using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController2 : MonoBehaviour {

	public GameObject[] spawnLocs, deSpawnLocs, stopLocs;
	public GameObject carPrefab, carParent;
	public float spawnInterval;
	//public List<GameObject> lane1, lane2, lane3, lane4, stopQ;
	public List<GameObject> stopQ;
	public Lane[] lanes;

	Lane lane1, lane2, lane3, lane4;

	// Use this for initialization
	void Start () {
		spawnInterval = 3f;
		lane1 = new Lane(15, 0);
		lane2 = new Lane(15, 1);
		lane3 = new Lane(15, 2);
		lane4 = new Lane(15, 3);
		stopQ = new List<GameObject>();
		lanes = new Lane[4];
		lanes[0] = lane1;
		lanes[1] = lane2;
		lanes[2] = lane3;
		lanes[3] = lane4;
		StartCoroutine(SpawnCar());
	}

	private IEnumerator SpawnCar() {
		yield return new WaitForSeconds(spawnInterval);
		//spawnInterval = 2f;
		int rand = Random.Range(0, lanes.Length - 1);
		print("Spawning car");
		GameObject currCar;
		Car2 car;
		print(lanes[rand].full);
		//If the lane pool is not full, instantiate a new one
		if (!lanes[rand].full) {
			currCar = Instantiate(carPrefab, spawnLocs[rand].transform.position, Quaternion.identity, carParent.transform);
			//rotateCar(CreateCar(currCar, rand), rand);
			car = currCar.GetComponent<Car2>();
			lanes[rand].cars[lanes[rand].currChildToSpawn] = currCar;
			car.carID = lanes[rand].currChildToSpawn;
			car.lane = rand;
			if (lanes[rand].currChildToSpawn == lanes[rand].maxChildren) lanes[rand].full = true;
		
		//If it is full, pool the last one back to the spawn pos
		}else {
			currCar = lanes[rand].cars[lanes[rand].currChildToSpawn];
			car = currCar.GetComponent<Car2>();
			currCar.SetActive(true);
			currCar.transform.position = spawnLocs[rand].transform.position;
			
		}
		lanes[rand].currChildToSpawn = lanes[rand].currChildToSpawn >= lanes[rand].maxChildren ? 0 : lanes[rand].currChildToSpawn++;
		rotateCar(car, rand);
		StartCoroutine(SpawnCar());
	}

	private void rotateCar(Car2 car, int rand) {
		//Set stop location
		Vector3 stopDest = stopLocs[rand].transform.position;
		car.stopLocation = stopDest;
		//Set rotation of car
		int carsInLane = lanes[rand].carsInLane;
		switch (rand) {
			case 0:
				car.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
				stopDest.x -= (carsInLane * 3f);
				break;
			case 1:
				car.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
				stopDest.z -= (carsInLane * 3f);
				break;
			case 2:
				car.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270f, 0));
				stopDest.z += (carsInLane * 3f);
				break;
			case 3:
				car.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
				stopDest.x += (carsInLane * 3f);
				break;
			default:
				Debug.Log("No lane at: " + rand);
				break;
		}
		car.despawnLocation = deSpawnLocs[rand].transform.position;
		car.nextDestination = stopDest;
	}
}
