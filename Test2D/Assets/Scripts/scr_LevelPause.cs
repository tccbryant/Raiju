using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_LevelPause : MonoBehaviour {

	public Canvas cvs_Paused;

	private bool paused = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause")) // pause game
			paused = !paused;

		if (paused) {
			cvs_Paused.enabled = true;
		} else {
			cvs_Paused.enabled = false;
		}
			
	}

	public void btn_Resume_OnClick(){
		paused = !paused;
	}

	public void btn_Options_OnClick(){

	}

	public void btn_QuitToMenu_OnClick(){
		SceneManager.LoadScene ("MainMenu");
	}
}
