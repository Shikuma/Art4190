using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour {
	public GameObject personPrefab, parentObject, destinationObject;
	public GameObject[] spawnLocations, destinationLocations;

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnPerson());
	}

	private IEnumerator SpawnPerson() {
		float time = GameObject.FindWithTag("GameController").GetComponent<DayNightController>().currentTimeOfDay;
		yield return new WaitForSeconds((Mathf.Abs(time - 0.5f) + 0.1f) * 5);

		int rand = Random.Range(0, spawnLocations.Length);
		GameObject person = Instantiate(personPrefab, spawnLocations[rand].transform.position, Quaternion.identity, parentObject.transform);
		PersonController pc = person.GetComponent<PersonController>();
		pc.goalObject = destinationLocations[rand];

		StartCoroutine(SpawnPerson());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
