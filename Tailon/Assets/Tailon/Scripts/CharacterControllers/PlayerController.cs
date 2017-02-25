using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    //Anim
    Animator anim;

    //Attack
    public bool _melee = true;
    public bool _range = false;
    public float _meleeDamage;
    public float _rangeDamage;

    //Assets
    public GameObject _bow;
    public GameObject _sword;
    public ParticleSystem _dashEmitter;
    public ParticleSystem _hitEmitter;

    //Stats
    public int _level;
    public float _currentLevelExp;
    public float _nextLevelExp;
    private float _expDifference;
    public EnemyKillEvent _enemyKill;
    public float _health;

    //GUI
    public Image _healthBar;
    public Image _dash1;
    public Image _dash2;
    public Image _dash3;
    public Text  _lvlText;

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
        anim = GetComponent<Animator>();
        _healthBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._healthBar;
        _dash1 = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._dash1;
        _dash2 = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._dash2;
        _dash3 = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._dash3;
        _lvlText = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._lvlText;
    }
    void OnDestroy()
    {
        if (_health <= 0)
        {
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

        // Animate the player.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Animating(h, v);
        _healthBar.fillAmount = _health / 100f;
        _lvlText.text = "Level: " + _level;
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }

    void dash()
    {
        switch (_dashNum)
        {
            case 0:
                _dash1.enabled = false;
                _dash2.enabled = false;
                _dash3.enabled = false;
                break;
            case 1: 
                _dash1.enabled = true;
                _dash2.enabled = false;
                _dash3.enabled = false;
                break;
            case 2: 
                _dash1.enabled = true;
                _dash2.enabled = true;
                _dash3.enabled = false;
                break;
            case 3: 
                _dash1.enabled = true;
                _dash2.enabled = true;
                _dash3.enabled = true;
                break;
        }
        if (_dashNum < 3)
        {
            _dashTimer += Time.deltaTime;
            if (_dashTimer >= _dashRegen)
            {
                _dashNum++;
                _dashTimer = 0.0f;
            }
        }
        if (_dashNum > 0)
        {
			if (Input.GetButtonDown("Dash") && Input.GetAxis("Horizontal") < 0)
            {
                _rgb.AddForce(-gameObject.transform.right * _dash);
                _dashNum--;
                _dashEmitter.Play();
            }
			else if (Input.GetButtonDown("Dash") && Input.GetAxis("Horizontal") > 0)
            {
                _rgb.AddForce(gameObject.transform.right * _dash);
                _dashNum--;
                _dashEmitter.Play();
            }
			else if (Input.GetButtonDown("Dash") && Input.GetAxis("Vertical") > 0)
            {
                _rgb.AddForce(gameObject.transform.forward * _dash);
                _dashNum--;
                _dashEmitter.Play();
            }
			else if (Input.GetButtonDown("Dash") && Input.GetAxis("Vertical") < 0)
            {
                _rgb.AddForce(-gameObject.transform.forward * _dash);
                _dashNum--;
                _dashEmitter.Play();
            }
        }
    }
    void jump()
    {
		if (Input.GetButtonDown("Jump") && _canJump)
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


        if (_melee)
            _sword.GetComponent<SkinnedMeshRenderer>().enabled = true;
        else
            _sword.GetComponent<SkinnedMeshRenderer>().enabled = false;

        if (_range)
            _bow.GetComponent<SkinnedMeshRenderer>().enabled = true;
        else
            _bow.GetComponent<SkinnedMeshRenderer>().enabled = false;    
    }


    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "floor")
        {
            _canJump = true;
        }
    }

}
