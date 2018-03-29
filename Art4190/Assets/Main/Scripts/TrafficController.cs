using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour {

	public GameObject[] spawnLocs, deSpawnLocs, stopLocs;
	public GameObject carPrefab, carParent;
	public float spawnInterval;
	public Queue<GameObject> lane1, lane2, lane3, lane4, stopQ;
	public Queue<GameObject>[] lanes;


	// Use this for initialization
	void Start () {
		spawnInterval = 3f;
		lane1 = new Queue<GameObject>();
		lane2 = new Queue<GameObject>();
		lane3 = new Queue<GameObject>();
		lane4 = new Queue<GameObject>();
		stopQ = new Queue<GameObject>();
		lanes = new Queue<GameObject>[4];
		lanes[0] = lane1;
		lanes[1] = lane2;
		lanes[2] = lane3;
		lanes[3] = lane4;

		StartCoroutine(SpawnCar());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator SpawnCar() {
		yield return new WaitForSeconds(spawnInterval);
		print("Sparning car");
		int rand = Random.Range(0, 3);
		GameObject currCar = Instantiate(carPrefab, spawnLocs[rand].transform.position, Quaternion.identity, carParent.transform);
		Car car = currCar.GetComponent<Car>();
		car.lane = rand;
		//Add to queue
		lanes[rand].Enqueue(currCar);

		//Set stop location
		Vector3 stopDest = stopLocs[rand].transform.position;
		//Set rotation of car
		switch (rand) {
			case 0:
				currCar.transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
				stopDest.x -= (lanes[rand].Count * 3f);
				break;
			case 1:
				currCar.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
				stopDest.z -= (lanes[rand].Count * 3f);
				break;
			case 2:
				currCar.transform.rotation = Quaternion.Euler(new Vector3(0, 270f, 0));
				stopDest.z += (lanes[rand].Count * 3f);
				break;
			case 3:
				currCar.transform.rotation = Quaternion.Euler(Vector3.zero);
				stopDest.x += (lanes[rand].Count * 3f);
				break;
			default:
				Debug.Log("No lane at: " + rand);
				break;
		}
		car.nextDest = stopDest;

	}
}
