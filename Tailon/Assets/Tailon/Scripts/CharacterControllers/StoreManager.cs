using UnityEngine;
using System.Collections;

public class StoreManager : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    Camera camera;
    public int healthPotPrice;
    private PlayerController _playerController;
    public TextMesh priceText;

	// Use this for initialization
	void Start () {
        priceText.text = healthPotPrice.ToString();
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
                priceText.color = Color.green;
                if (_playerController && Input.GetKeyDown(KeyCode.E))
                {
                    _playerController.healthPots++;
                    _playerController.money -= healthPotPrice;
                    _playerController.healthPotsText.text = _playerController.healthPots.ToString();
                }
            }
            else
            {
                priceText.color = Color.red;
            }
        }
    }
}
