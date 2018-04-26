using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {
	public GameObject[] standardObjects, userObjects;
	private bool isDragging = false, isStopSignSelected, isLightPoleSelected;
	private float yPos;
	private GameObject draggingObject, selectedObject;
	public GameObject stopSign, streetLight, flower, removeButton;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
			
		if (Physics.Raycast(ray, out hit)) {
			if (isDragging) {
				print(hit.transform.gameObject.name);
				if (Input.GetMouseButtonDown(0)) {
					isDragging = false;
					isStopSignSelected = false;
					isLightPoleSelected = false;
					if ((hit.transform.gameObject.tag == "stopSign" && draggingObject.gameObject.tag == "stopSign") || (hit.transform.gameObject.tag == "lightPole" && draggingObject.gameObject.tag == "lightPole")) {
						hit.transform.GetChild(0).gameObject.SetActive(true);
					}
					Destroy(draggingObject);
				}
				else {
					if (isStopSignSelected) {
						draggingObject = Instantiate(stopSign, Vector3.zero, Quaternion.identity);
						isStopSignSelected = false;
						yPos = 1f;
					}
					else if (isLightPoleSelected) {
						draggingObject = Instantiate(streetLight, Vector3.zero, Quaternion.identity);
						isLightPoleSelected = false;
						yPos = 2f;
					}
				}

				if(draggingObject != null)
				draggingObject.transform.position = new Vector3(hit.point.x,
																yPos,
																hit.point.z);
			}else if (Input.GetMouseButtonDown(0)) {
				if(hit.transform.gameObject.tag == "stopSign" || hit.transform.gameObject.tag == "lightPole") {
					selectedObject = hit.transform.gameObject;
					removeButton.SetActive(true);
				}
				print(hit.transform.gameObject.name);
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

	public void RemoveSelected() {
		selectedObject.transform.GetChild(0).gameObject.SetActive(false);
		removeButton.SetActive(false);
	}
}
