﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
	public GameObject hud;
	public GameObject menu;
	private bool onPause = false;


	void Start () {
		menu.gameObject.SetActive(false);
		hud.gameObject.SetActive(true);
		Time.timeScale = 1.0f;
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale == 1.0f) {
				menu.gameObject.SetActive (true);
				hud.gameObject.SetActive (false);
				Time.timeScale = 0.0f;
				onPause = true;
			}
			else{
			resume ();
			}
		}

		if (onPause) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		else
			Cursor.visible = false;
	}

	public void exit()
	{
		Application.Quit();
	}

	public void mainMenu()
    {
        Time.timeScale = 1.0f;
		SceneManager.LoadScene("Menu");
	}

	public void resume()
	{
		menu.gameObject.SetActive (false);
		hud.gameObject.SetActive (true);
		Time.timeScale = 1.0f;
		onPause = false;
	}
}