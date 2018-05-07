using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class TrafficController2 : MonoBehaviour {

	public GameObject[] spawnLocs, deSpawnLocs, stopLocs, stopSigns, carPrefabs;
	public GameObject carParent;
	public float spawnInterval, speed, minSpeed, maxSpeed, speedMPH;
	//public List<GameObject> lane1, lane2, lane3, lane4, stopQ;
	public GameObject[] stopQ;
	public Lane[] lanes;
	public int carsInStopQ = 0, previousLane = 0;

	Lane lane1, lane2, lane3, lane4;

	public bool[] stops;

	public Text speedText;
	public Button increaseBtn, decreaseBtn;

	// Use this for initialization
	void Start () {
		spawnInterval = 3f;
		lane1 = new Lane(10, 0);
		lane2 = new Lane(10, 1);
		lane3 = new Lane(10, 2);
		lane4 = new Lane(10, 3);
		stopQ = new GameObject[4];
		lanes = new Lane[4];
		lanes[0] = lane1;
		lanes[1] = lane2;
		lanes[2] = lane3;
		lanes[3] = lane4;
		StartCoroutine(SpawnCar());
		stops = new bool[4];

		speedMPH = speed;
		speedText.text = "" + speedMPH + " MPH";
		setMPS(speed);
	}
	private void helper() {
		foreach(Lane lane in lanes){
			foreach(GameObject car in lane.cars){
				if(car != null)
					car.GetComponent<NavMeshAgent>().speed = speed;
			}
		}
	}
	public void IncreaseSpeed(){
		if( getMPH() + 1 < maxSpeed){
			speedMPH++;
			speedText.text = "" + speedMPH + " MPH";
			setMPS(speedMPH);
			helper();
		}
		if(getMPH() + 1 >= maxSpeed){
			increaseBtn.image.color = new Color(1,1,1,0.2f);
			increaseBtn.interactable = false;
		}else{
			decreaseBtn.image.color = new Color(1,1,1,1);
			decreaseBtn.interactable = true;
		}
	}
	public void DecreaseSpeed(){
		if(getMPH() > minSpeed){
			speedMPH--;
			speedText.text = "" + speedMPH + " MPH";
			setMPS(speedMPH);
			helper();
		}
		if(getMPH() <= minSpeed) {
			decreaseBtn.image.color = new Color(1,1,1, 0.2f);
			decreaseBtn.interactable = false;
		} else {
			increaseBtn.image.color = new Color(1,1,1,1);
			increaseBtn.interactable = true;
		}
	}

	public float getMPH(){
		return speed*2.23694f;
	}
	public void setMPS(float MPH){
		speed = MPH * 0.44f;
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
		if (spawnInterval == 0 && rand == previousLane) rand++;
		if (rand == lanes.Length) rand = 0;
		//print("Spawning car");
		GameObject currCar;
		Car2 car;
			
		currCar = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], spawnLocs[rand].transform.position, Quaternion.identity, carParent.transform);
		//rotateCar(CreateCar(currCar, rand), rand);
		car = currCar.GetComponent<Car2>();
		currCar.GetComponent<NavMeshAgent>().speed = speed;
		if (lanes[rand].full) lanes[rand].cars[lanes[rand].currChildToSpawn] = currCar;
		else lanes[rand].cars.Add(currCar);
		car.carID = lanes[rand].currChildToSpawn;
		car.lane = rand;
		if (lanes[rand].currChildToSpawn + 1 == lanes[rand].maxChildren) {
			lanes[rand].full = true;
		}

		lanes[rand].carsInLane++;
		lanes[rand].currChildToSpawn = lanes[rand].currChildToSpawn >= lanes[rand].maxChildren-1 ? 0 : lanes[rand].currChildToSpawn+1;
		rotateCar(car, rand);
		UpdateCarsInFront(car.carID, car.lane, lanes[rand].carsInLane);
		spawnInterval = spawnInterval == 0f ? 3f : 0f;
		previousLane = rand;
		StartCoroutine(SpawnCar());
	}

	private void UpdateCarsInFront(int carID, int lane, int carsInLane) {
		for(int i = 1; i < carsInLane; i++) {
			int nextCar = 0;
			nextCar = carID - i < 0 ? lanes[lane].maxChildren + carID - i : carID - i;
			if (nextCar > lanes[lane].cars.Capacity){ // || !lanes[lane].cars[nextCar]) {
				continue;
			}
			Car2 car = lanes[lane].cars[nextCar].GetComponent<Car2>();
			switch (lane) {
				case 0:
					if (car.nextDestination.x != car.stopLocation.x - ((carsInLane - i - 1) * 3f)) {
						car.nextDestination.x = car.stopLocation.x - ((carsInLane - i - 1) * 3f);
					}
					break;
				case 1:
					if (car.nextDestination.z != car.stopLocation.z - ((carsInLane - i - 1) * 3f)) {
						car.nextDestination.z = car.stopLocation.z - ((carsInLane - i - 1) * 3f);
					}
					break;
				case 2:
					if (car.nextDestination.z != car.stopLocation.z + ((carsInLane - i - 1) * 3f)) {
						car.nextDestination.z = car.stopLocation.z + ((carsInLane - i - 1) * 3f);
					}
					break;
				case 3:
					if (car.nextDestination.x != car.stopLocation.x + ((carsInLane - i - 1) * 3f)) {
						car.nextDestination.x = car.stopLocation.x + ((carsInLane - i - 1) * 3f);
					}
					break;
				default:
					break;
			}

		}
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
