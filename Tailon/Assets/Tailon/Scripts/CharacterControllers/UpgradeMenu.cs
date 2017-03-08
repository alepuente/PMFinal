using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

    public Button[] buttons;
    public Text pointsText;
	// Use this for initialization
	void Start () {
        pointsText.text = DungeonStates.instance.upgradePoints.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        if (DungeonStates.instance.upgradePoints>0)
        {
            pointsText.text = DungeonStates.instance.upgradePoints.ToString();
            foreach (var item in buttons)
            {
                item.interactable = true;
            }
        }	
	}
    void disableButtons()
    {
        pointsText.text = DungeonStates.instance.upgradePoints.ToString();
        if (DungeonStates.instance.upgradePoints == 0)
        {
            foreach (var item in buttons)
            {
                item.interactable = false;
            } 
        }       
    }

    public void upgradeHealth()
    {
        DungeonStates.instance._healthRestorage += 5;
        DungeonStates.instance.upgradePoints--;
        disableButtons();
        DungeonStates.instance.saveStats();
    }

    public void upgradeStamina()
    {
        DungeonStates.instance._staminaRestorage += 2;
        DungeonStates.instance.upgradePoints--;
        disableButtons();
        DungeonStates.instance.saveStats();
    }

    public void upgradeRange()
    {
        DungeonStates.instance._rangeDamage += 2;
        DungeonStates.instance.upgradePoints--;
        disableButtons();
        DungeonStates.instance.saveStats();
    }

    public void upgradeMelee()
    {
        DungeonStates.instance._meleeDamage += 5;
        DungeonStates.instance.upgradePoints--;
        disableButtons();
        DungeonStates.instance.saveStats();
    }
}
