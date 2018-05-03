using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour {

	public GameObject[] pages;
	public int currPage;
	public Text currText;

	// Use this for initialization
	void Start () {
		currText.text = ""+ (currPage + 1) + ".";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IncrementPages(int value) {
		pages[currPage].SetActive(false);
		currPage += value;
		if (currPage >= pages.Length) currPage = 0;
		else if (currPage < 0) currPage = pages.Length - 1;
		pages[currPage].SetActive(true);
		currText.text = "" + (currPage + 1) + ".";
	}
}
