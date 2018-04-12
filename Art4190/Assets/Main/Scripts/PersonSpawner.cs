using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour {
	public GameObject personPrefab, parentObject, destinationObject;
	public float spawnInterval;

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnPerson());
	}

	private IEnumerator SpawnPerson() {
		yield return new WaitForSeconds(spawnInterval);

		GameObject person = Instantiate(personPrefab, gameObject.transform.position, Quaternion.identity, parentObject.transform);
		PersonController pc = person.GetComponent<PersonController>();
		pc.goalObject = destinationObject;
		StartCoroutine(SpawnPerson());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
