using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController2 : MonoBehaviour {

	public GameObject[] spawnLocs, deSpawnLocs, stopLocs, stopSigns;
	public GameObject carPrefab, carParent;
	public float spawnInterval;
	//public List<GameObject> lane1, lane2, lane3, lane4, stopQ;
	public GameObject[] stopQ;
	public Lane[] lanes;
	public int carsInStopQ = 0;

	Lane lane1, lane2, lane3, lane4;

	public bool[] stops;

	// Use this for initialization
	void Start () {
		spawnInterval = 3f;
		lane1 = new Lane(5, 0);
		lane2 = new Lane(5, 1);
		lane3 = new Lane(5, 2);
		lane4 = new Lane(5, 3);
		stopQ = new GameObject[4];
		lanes = new Lane[4];
		lanes[0] = lane1;
		lanes[1] = lane2;
		lanes[2] = lane3;
		lanes[3] = lane4;
		StartCoroutine(SpawnCar());
		stops = new bool[4];
	}

	private void Update() {
		if (!stops[0] && stopSigns[0].activeSelf)
			stops[0] = true;
		else if (stops[0] && !stopSigns[0].activeSelf)
			stops[0] = false;

		if (!stops[1] && stopSigns[1].activeSelf)
			stops[1] = true;
		else if (stops[1] && !stopSigns[1].activeSelf)
			stops[1] = false;

		if (!stops[2] && stopSigns[2].activeSelf)
			stops[2] = true;
		else if (stops[2] && !stopSigns[2].activeSelf)
			stops[2] = false;

		if (!stops[3] && stopSigns[3].activeSelf)
			stops[3] = true;
		else if (stops[3] && !stopSigns[3].activeSelf)
			stops[3] = false;
	}

	private IEnumerator SpawnCar() {
		yield return new WaitForSeconds(spawnInterval);

		//uncomment to spawn 1 car
		//spawnInterval = 200000f;

		int rand = Random.Range(0, lanes.Length - 1);
		//print("Spawning car");
		GameObject currCar;
		Car2 car;
		
		currCar = Instantiate(carPrefab, spawnLocs[rand].transform.position, Quaternion.identity, carParent.transform);
		//rotateCar(CreateCar(currCar, rand), rand);
		car = currCar.GetComponent<Car2>();
		lanes[rand].cars[lanes[rand].currChildToSpawn] = currCar;
		car.carID = lanes[rand].currChildToSpawn;
		car.lane = rand;
		if (lanes[rand].currChildToSpawn + 1 == lanes[rand].maxChildren) {
			print("LANE " + rand + " IS FULL");
			lanes[rand].full = true;
		}

		lanes[rand].carsInLane++;
		lanes[rand].currChildToSpawn = lanes[rand].currChildToSpawn >= lanes[rand].maxChildren-1 ? 0 : lanes[rand].currChildToSpawn+1;
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
				stopDest.x -= ((carsInLane-1) * 3f);
				break;
			case 1:
				car.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
				stopDest.z -= ((carsInLane-1) * 3f);
				break;
			case 2:
				car.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270f, 0));
				stopDest.z += ((carsInLane-1) * 3f);
				break;
			case 3:
				car.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
				stopDest.x += ((carsInLane-1) * 3f);
				break;
			default:
				Debug.Log("No lane at: " + rand);
				break;
		}
		car.despawnLocation = deSpawnLocs[rand].transform.position;
		car.nextDestination = stopDest;
	}
}
