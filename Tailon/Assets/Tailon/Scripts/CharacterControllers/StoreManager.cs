using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    Camera camera;
    public int healthPotPrice;
    public int staminaPotPrice;
    private PlayerController _playerController;
    public TextMesh priceTextHeatlh;
    public TextMesh priceTextStamina;

	// Use this for initialization
	void Start () {
        priceTextHeatlh.text = healthPotPrice.ToString();
        priceTextStamina.text = staminaPotPrice.ToString();
        camera = Camera.main;
        if (GameObject.Find("PlayerAnim"))
        {
            _playerController = GameObject.Find("PlayerAnim").GetComponent<PlayerController>();
        }
	}
	
	// Update is called once per frame
    void Update()
    {
        ray = camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "HealthPot" && DungeonStates.instance._money >= healthPotPrice)
            {
                priceTextHeatlh.color = Color.green;
                if (_playerController && Input.GetKeyDown(KeyCode.E))
                {
                    DungeonStates.instance._healthPots++;
                    DungeonStates.instance._money -= healthPotPrice;
                    _playerController.refreshHUD();
                    DungeonStates.instance.saveStats();
                }
            }
            else if (hit.transform.tag == "StaminaPot" && DungeonStates.instance._money >= staminaPotPrice)
            {
                priceTextStamina.color = Color.green;
                if (_playerController && Input.GetKeyDown(KeyCode.E))
                {
                    DungeonStates.instance._staminaPots++;
                   DungeonStates.instance._money -= staminaPotPrice;
                   _playerController.refreshHUD();
                   DungeonStates.instance.saveStats();
                }
            }
            else
            {
                priceTextHeatlh.color = Color.red;
                priceTextStamina.color = Color.red;
            }
        }
    }
}
