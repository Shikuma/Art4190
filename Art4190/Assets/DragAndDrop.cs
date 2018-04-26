using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {
	public GameObject[] standardObjects, userObjects;
	private bool isDragging = false, isStopSignSelected, isLightPoleSelected;
	private float yPos;
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
					yPos = 1f;
				}else if(isLightPoleSelected){
					draggingObject = Instantiate(streetLight, Vector3.zero, Quaternion.identity);
					isLightPoleSelected = false;
					yPos = 2f;
				}
			}
            if (Physics.Raycast(ray, out hit)) {
				draggingObject.transform.position = new Vector3(hit.point.x,
																yPos,
																hit.point.z);
            }
			
		}
	}

	public void stopSignCreate() {
		isDragging = true;
		isStopSignSelected = true;
	}

	public void lightPoleCreate() {
		isDragging = true;
		isLightPoleSelected = true;
	}
}
