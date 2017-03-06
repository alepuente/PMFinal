using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class EnemyKillEvent : UnityEvent<float, int>
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


    public Text moneyText;

    public Text healthPotsText;

    public Text staminaPotsText;
    public bool onStaminaPot;
    public float staminaTimer;
    public float staminaPotDuration;

    void Start()
    {
        _rgb = gameObject.GetComponent<Rigidbody>();
        _level = DungeonStates.instance._playerLevel;
        _currentLevelExp = DungeonStates.instance._playerCurrentLevelExp;
        _nextLevelExp = DungeonStates.instance._playerNextLevelExp;
        _enemyKill = new EnemyKillEvent();
        _enemyKill.AddListener(enemyExp);
        _health = DungeonStates.instance._playerHealth;
        anim = GetComponent<Animator>();
        _healthBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._healthBar;
        _dash1 = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._dash1;
        _dash2 = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._dash2;
        _dash3 = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._dash3;
        _lvlText = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._lvlText;
        _lvlText = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._lvlText;

        moneyText = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._money;

        healthPotsText = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._healthPots;
        healthPotsText.text = DungeonStates.instance._healthPots.ToString();

        staminaPotsText = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._staminaPots;
        staminaPotsText.text = DungeonStates.instance._staminaPots.ToString();

        _meleeDamage = DungeonStates.instance._meleeDamage;
        _rangeDamage = DungeonStates.instance._rangeDamage;

        refreshHUD();
    }
    void OnDestroy()
    {
        if (_health <= 0)
        {
            DungeonStates.instance.restartStates();
            DungeonStates.instance.resetItems();
            SceneManager.LoadScene("Lobby");
        }
    }
    void enemyExp(float exp, int _money)
    {
        _currentLevelExp += exp;
        DungeonStates.instance._money += _money;
        DungeonStates.instance.saveStats();
        refreshHUD();
    }
    void lvlUp()
    {
        if (_currentLevelExp >= _nextLevelExp)
        {
            _expDifference = _currentLevelExp - _nextLevelExp;
            _currentLevelExp = _expDifference;
            _level++;
            _nextLevelExp = _nextLevelExp * 1.20f;
            DungeonStates.instance._playerLevel = _level;
            DungeonStates.instance._playerCurrentLevelExp = _currentLevelExp;
            DungeonStates.instance._playerNextLevelExp = _nextLevelExp;
        }
    }
    void Update()
    {
        lvlUp();
        movement();
        jump();
        dash();
        weapons();
        Pots();
        // Animate the player.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Animating(h, v);
        _healthBar.fillAmount = _health / 100f;
        _lvlText.text = "Level: " + _level;
    }

    public void refreshHUD()
    {
        moneyText.text = DungeonStates.instance._money.ToString();
        healthPotsText.text = DungeonStates.instance._healthPots.ToString();
        staminaPotsText.text = DungeonStates.instance._staminaPots.ToString();
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

    void Pots()
    {
        if (Input.GetKeyDown(KeyCode.F) && DungeonStates.instance._healthPots > 0 && (_health + DungeonStates.instance._healthRestorage) < DungeonStates.instance._playerHealth)
        {
            DungeonStates.instance._healthPots--;
            _health += DungeonStates.instance._healthRestorage;
            refreshHUD();
        }
        if (Input.GetKey(KeyCode.G) && DungeonStates.instance._staminaPots > 0 && !onStaminaPot)
        {
            DungeonStates.instance._staminaPots--;
            onStaminaPot = true;
            refreshHUD();
        }
        if (onStaminaPot)
        {
            staminaTimer += Time.deltaTime;
            if (staminaTimer >= staminaPotDuration)
            {
                staminaTimer = 0;
                onStaminaPot = false;
            }
        }
        else
        {
            staminaTimer = 0;
        }
    }


    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "floor")
        {
            _canJump = true;
        }
        if (hit.gameObject.tag == "HealthPotDrop")
        {
            Destroy(hit.gameObject);
            DungeonStates.instance._healthPots++;
            refreshHUD();
        }
        if (hit.gameObject.tag == "StaminaPotDrop")
        {
            Destroy(hit.gameObject);
            DungeonStates.instance._staminaPots++;
            refreshHUD();
        }
    }


    
}
