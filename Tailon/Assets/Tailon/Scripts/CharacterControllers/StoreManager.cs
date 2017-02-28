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
            if (hit.transform.tag == "HealthPot" && _playerController.money>=healthPotPrice)
            {
                priceTextHeatlh.color = Color.green;
                if (_playerController && Input.GetKeyDown(KeyCode.E))
                {
                    _playerController.healthPots++;
                    DungeonStates.instance._healthPots++;
                    _playerController.money -= healthPotPrice;
                    _playerController.healthPotsText.text = _playerController.healthPots.ToString();
                }
            }
            else if (hit.transform.tag == "StaminaPot" && _playerController.money >= staminaPotPrice)
            {
                priceTextStamina.color = Color.green;
                if (_playerController && Input.GetKeyDown(KeyCode.E))
                {
                    _playerController.staminaPots++;
                    DungeonStates.instance._staminaPots++;
                    _playerController.money -= staminaPotPrice;
                    _playerController.staminaPotsText.text = _playerController.staminaPots.ToString();
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
