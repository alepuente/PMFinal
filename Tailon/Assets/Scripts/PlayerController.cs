using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EnemyKillEvent : UnityEvent<float>
{
}

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
    public float _currentLevelExp;
    public float _nextLevelExp;
    private float _expDifference;
    public EnemyKillEvent _enemyKill;
    public float _health;


	//Game/Dungeon Controller Reference
	public DungeonStates _dungeonController;

    void Start()
    {
        _rgb = gameObject.GetComponent<Rigidbody>();
		_level = _dungeonController._playerLevel;
        _currentLevelExp = _dungeonController._playerCurrentLevelExp;
        _nextLevelExp = _dungeonController._playerNextLevelExp;
        _enemyKill = new EnemyKillEvent();
        _enemyKill.AddListener(enemyExp);
        _health = _dungeonController._playerHealth;
    }
    void OnDestroy()
    {
		if (_health<=0) {
			_dungeonController.restartStates();     
			SceneManager.LoadScene("Lobby");
		}
    }
    void enemyExp(float exp)
    {
        _currentLevelExp += exp;   
    }
    void lvlUp()
    {
        if (_currentLevelExp >= _nextLevelExp)
        {
            _expDifference = _currentLevelExp - _nextLevelExp;
            _currentLevelExp = _expDifference;
            _level++;
            _nextLevelExp = _nextLevelExp * 1.20f;
            _dungeonController._playerLevel = _level;
            _dungeonController._playerCurrentLevelExp = _currentLevelExp;
            _dungeonController._playerNextLevelExp = _nextLevelExp;
        } 
    }
    void Update()
    {
        lvlUp();
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
            gameObject.transform.Translate(Vector3.left * _velocity * Time.deltaTime);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            gameObject.transform.Translate(Vector3.right * _velocity * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            gameObject.transform.Translate(Vector3.forward * _velocity * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            gameObject.transform.Translate(Vector3.back * _velocity * Time.deltaTime);
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
