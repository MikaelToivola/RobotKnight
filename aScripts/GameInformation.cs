using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInformation : MonoBehaviour {

	bool MenuVisible = false;

	void Awake () {
		DontDestroyOnLoad (this);
		
	}
	void Update(){
		if (Input.GetKey (KeyCode.Escape) && MenuVisible == false) {
			Time.timeScale = 0.001f;
			Cursor.visible = true;
			MenuVisible = true;
		} else if (Input.GetKey (KeyCode.Escape) && MenuVisible == true) {
			Time.timeScale = 1f;
			Cursor.visible = false;
			MenuVisible = false;
		}
	}
	void OnGUI(){
		if (MenuVisible) {
			ShowMenu ();
		}
	}
	private void ShowMenu(){
		if (GUI.Button (new Rect (Screen.width - (Screen.width/2), Screen.height - (Screen.height/2), 200, 60), "Restart")) {

			SceneManager.LoadScene ("level0");
			Destroy (this);
		}
		if (GUI.Button (new Rect (Screen.width - (Screen.width/2), Screen.height - (Screen.height/2)+ 100, 200, 60), "Quit")) {

			Application.Quit();
		}
	}

}
