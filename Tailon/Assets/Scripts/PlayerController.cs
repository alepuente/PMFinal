using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //Movement
    public float _velocity = 0.1f;
    public float _jump = 300f;
    public float _dash = 500f;
    private Rigidbody _rgb;
    private bool _canJump;
    public int _dashNum;
    public float _dashRegen;
    private float _dashTimer;

    //Attack
    public bool _melee = true;
    public bool _range = false;
    public float _meleeDamage;
    public float _rangeDamage;

    //Assets
    public GameObject _bow;
    public GameObject _sword;

	//Stats
	public int _level;
	public int _health;

	//Game/Dungeon Controller Reference
	public DungeonStates _dungeonController;

    void Start()
    {
        _rgb = gameObject.GetComponent<Rigidbody>();
		_level = _dungeonController.playerLevel;
		_health = 100;
    }
    void Update()
    {
        movement();
        jump();
        dash();
        weapons();
    }
    void dash()
    {
        if (_dashNum<3)
        {
            _dashTimer += Time.deltaTime;
            if (_dashTimer>=_dashRegen)
            {
                _dashNum++;
                _dashTimer = 0.0f;
            }
        }
        if (_dashNum > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Horizontal") < 0)
            {
                _rgb.AddForce(-gameObject.transform.right * _dash);
                _dashNum--;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Horizontal") > 0)
            {
                _rgb.AddForce(gameObject.transform.right * _dash);
                _dashNum--;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0)
            {
                _rgb.AddForce(gameObject.transform.forward * _dash);
                _dashNum--;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Vertical") < 0)
            {
                _rgb.AddForce(-gameObject.transform.forward * _dash);
                _dashNum--;
            }
        }
    }
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            _rgb.AddForce(0, _jump, 0);
            _canJump = false;
        }
    }
    void movement()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            gameObject.transform.Translate(Vector3.left * _velocity);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            gameObject.transform.Translate(Vector3.right * _velocity);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            gameObject.transform.Translate(Vector3.forward * _velocity);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            gameObject.transform.Translate(Vector3.back * _velocity);
        }
    }
    void movementRGB()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            _rgb.AddForce(Vector3.left * _velocity);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            _rgb.AddForce(Vector3.right * _velocity);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            _rgb.AddForce(Vector3.forward * _velocity);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            _rgb.AddForce(Vector3.back * _velocity);
        }
    }
    void weapons()
    {
       // _bow.transform.localRotation = (GameObject.FindGameObjectWithTag("Head").transform.rotation * Quaternion.Euler(90,0,90));
        if (_melee)
            _sword.SetActive(true);
        else
            _sword.SetActive(false);

        if (_range)
            _bow.SetActive(true);
        else
            _bow.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_melee)
            {
                _melee = false;
                _range = true;
            }
            else
            {
                _melee = true;
                _range = false;
            }
        }
    }
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "floor")
        {
            _canJump = true;   
        }
    }
}
