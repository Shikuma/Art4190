using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Penis : MonoBehaviour {
	public GameObject destinationObject;
	private NavMeshAgent agent;
		
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.destination = destinationObject.transform.position;
		agent.updateRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
