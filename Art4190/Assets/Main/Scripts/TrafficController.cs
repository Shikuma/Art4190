using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour {

	public GameObject[] spawnLocs, deSpawnLocs, stopLocs;
	public GameObject carPrefab, carParent;
	public float spawnInterval;
	public List<GameObject> lane1, lane2, lane3, lane4, stopQ;
	public List<GameObject>[] lanes;


	// Use this for initialization
	void Start () {
		spawnInterval = 3f;
		lane1 = new List<GameObject>();
		lane2 = new List<GameObject>();
		lane3 = new List<GameObject>();
		lane4 = new List<GameObject>();
		stopQ = new List<GameObject>();
		lanes = new List<GameObject>[4];
		lanes[0] = lane1;
		lanes[1] = lane2;
		lanes[2] = lane3;
		lanes[3] = lane4;

		StartCoroutine(SpawnCar());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private Car createCar(GameObject currCar, int rand){
		Car car = currCar.GetComponent<Car>();
		car.lane = rand;
		//Add to queue
		lanes[rand].Add(currCar);	
		return car;
	}
	private void rotateCar(Car car, int rand) {
		//Set stop location
		Vector3 stopDest = stopLocs[rand].transform.position;
		car.nextDest = stopDest;
		//Set rotation of car
		switch (rand) {
			case 0:
				car.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
				stopDest.x -= ((lanes[rand].Count - 1) * 3f);
				break;
			case 1:
				car.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
				stopDest.z -= ((lanes[rand].Count - 1) * 3f);
				break;
			case 2:
				car.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 270f, 0));
				stopDest.z += ((lanes[rand].Count - 1) * 3f);
				break;
			case 3:
				car.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
				stopDest.x += ((lanes[rand].Count - 1) * 3f);
				break;
			default:
				Debug.Log("No lane at: " + rand);
				break;
		}
		car.deSpawnLoc = deSpawnLocs[rand].transform.position;
		car.tempStopLoc = stopDest;
	}

	private IEnumerator SpawnCar() {
		yield return new WaitForSeconds(spawnInterval);
		int rand = Random.Range(0, 1);
		print("Sparning car");

		GameObject currCar = Instantiate(carPrefab, spawnLocs[rand].transform.position, Quaternion.identity, carParent.transform);
		rotateCar(createCar(currCar,rand),rand);
		

		StartCoroutine(SpawnCar());
	}
}
