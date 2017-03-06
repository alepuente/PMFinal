using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public GameObject instructionsPanel;
    public GameObject tutorialPanel;
    public Button playButton;
    public Button exitButton;
    public Button instructionsButton;
    public Toggle neverAsk;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
                        
	}

    public void removePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
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
    public void showTutorial()
    {
        if (tutorialPanel.activeSelf)
        {
            tutorialPanel.SetActive(false);
            playButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
            instructionsButton.gameObject.SetActive(true);
        }
        else
        {
            playButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(false);
            instructionsButton.gameObject.SetActive(false);
            tutorialPanel.SetActive(true);
        } 
    }
    public void play()
    {
        if (PlayerPrefs.GetInt("tutorial") == 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (PlayerPrefs.GetInt("tutorial") == 1)
        {
            showTutorial();
        }
        else
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    public void exit()
    {
        Application.Quit();
    }

    public void toLobby()    {
        
        SceneManager.LoadScene("Lobby");
    }

    public void toTutorial(){
         SceneManager.LoadScene( "Tutorial");
     }

    public void neverAskAgain(bool toogle)
    {
        if (toogle)
        {
            PlayerPrefs.SetInt("tutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("tutorial", 2);
        }
        PlayerPrefs.Save();
    }
}
