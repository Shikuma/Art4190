using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenURL(string url) {
		Application.OpenURL(url);
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}
}
