using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonController : MonoBehaviour {
	public GameObject goalObject;

	private Vector3 goalLocation;
	private NavMeshAgent agent;


	// Use this for initialization
	void Start () {
		goalLocation = goalObject.transform.position;
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = goalLocation;
	}
}
