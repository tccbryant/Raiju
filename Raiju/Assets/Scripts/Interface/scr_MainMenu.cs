using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_MainMenu : MonoBehaviour
{
    // Use this for initialization
    void Start() {
		
    }

    // Update is called once per frame
    void Update() {

    }

    public void btn_NewGame_OnClick() {
        SceneManager.LoadScene("tile_test");
    }

	public void btn_Options_OnClick() {

	}

	public void btn_Exit_OnClick() {
		Application.Quit ();
	}

}
