using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //Movement
    public float _velocity = 0.1f;
    public float _jump = 300f;
    public float _dash = 500f;
    public GameObject basicAttack;
    private Rigidbody rgb;
    //Attack
    private GameObject[] _ammo;
    public float attackSpeed = 0.2f;
    private float timerAttack;
    public int _maxAmmo = 10;


    void Start()
    {
        rgb = gameObject.GetComponent<Rigidbody>();
        _ammo = new GameObject[_maxAmmo];
        for (int i = 0; i < _maxAmmo; i++)
        {
            GameObject obj = (GameObject)Instantiate(basicAttack);
            obj.SetActive(false);
            _ammo[i] = obj;
        }
    }

    void Update()
    {
        movement();
        jump();
        dash();
        shoot();
    }
    void shoot()
    {
        timerAttack += Time.deltaTime;
        if (Input.GetMouseButton(0)&&timerAttack>attackSpeed)
        {
            for (int i = 0; i < _ammo.Length; i++)
            {
                if (!_ammo[i].activeInHierarchy)
                {
                    _ammo[i].transform.position = gameObject.transform.position;
                    _ammo[i].transform.rotation = gameObject.transform.rotation;
                    _ammo[i].SetActive(true);
                    timerAttack = 0.0f;
                    break;
                }
            }
        }
        
    }
    void dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Horizontal") < 0)
        {
            rgb.AddForce(Vector3.left * _dash);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Horizontal") > 0)
        {
            rgb.AddForce(Vector3.right * _dash);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0)
        {
            rgb.AddForce(Vector3.forward * _dash);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Vertical") < 0)
        {
            rgb.AddForce(Vector3.back * _dash);
        }
    }
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rgb.AddForce(0, _jump, 0);
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
            rgb.AddForce(Vector3.left * _velocity);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
           rgb.AddForce(Vector3.right * _velocity);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
           rgb.AddForce(Vector3.forward * _velocity);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            rgb.AddForce(Vector3.back * _velocity);
        }
    }

}
