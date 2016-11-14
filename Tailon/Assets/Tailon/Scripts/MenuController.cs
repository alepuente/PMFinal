using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public GameObject instructionsPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void showInstructions()
    {
        if (instructionsPanel.activeSelf)
        {
            instructionsPanel.SetActive(false);
        }
        else
        {
            instructionsPanel.SetActive(true);
        }
    }


    public void exit()
    {
        Application.Quit();
    }

    public void toLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
