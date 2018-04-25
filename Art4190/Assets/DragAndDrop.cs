using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {
	public GameObject[] standardObjects, userObjects;
	private bool isDragging = false, isStopSignSelected;
	private GameObject draggingObject;
	public GameObject stopSign, streetLight, flower;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(isDragging) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

			if(Input.GetMouseButtonDown(0)) {
				isDragging = false;
				isStopSignSelected = false;
			}else{
				if(isStopSignSelected) {
					draggingObject = Instantiate(stopSign, Vector3.zero, Quaternion.identity);
					isStopSignSelected = false;
				}
			}


            if (Physics.Raycast(ray, out hit)) {
				
				draggingObject.transform.position = hit.point;
            }
			
		}
	}

	public void stopSignCreate() {
		isDragging = true;
		isStopSignSelected = true;
	}
}
