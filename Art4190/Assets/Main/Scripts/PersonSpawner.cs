using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour {
	public GameObject personPrefab, parentObject, destinationObject;
	[SerializeField]
	float spawnInterval, spawnMin, spawnMax;
	public GameObject[] spawnLocations, destinationLocations;

	// Use this for initialization
	void Start () {
		
		StartCoroutine(SpawnPerson());
	}

	private IEnumerator SpawnPerson() {
		spawnInterval = Random.Range(spawnMin, spawnMax);
		yield return new WaitForSeconds(spawnInterval);
		int rand = Random.Range(0, spawnLocations.Length);
		GameObject person = Instantiate(personPrefab, spawnLocations[rand].transform.position, Quaternion.identity, parentObject.transform);
		PersonController pc = person.GetComponent<PersonController>();
		pc.goalObject = destinationLocations[rand];

		if (spawnMin > 1f) spawnMin--;
		if (spawnMax > 5f) spawnMax--;

		StartCoroutine(SpawnPerson());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
