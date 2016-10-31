using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //Movement
    public float _velocity = 0.1f;
    public float _jump = 300f;
    public float _dash = 500f;   
    private Rigidbody rgb;
    private bool canJump;

    //Attack
    public bool _melee = true;
    public bool _range = false;
    public float _meleeDamage;
    public float __rangeDamage;


    void Start()
    {
        rgb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        movement();
        jump();
        dash();
    }

    void dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Horizontal") < 0)
        {
            rgb.AddForce(-gameObject.transform.right * _dash);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Horizontal") > 0)
        {
            rgb.AddForce(gameObject.transform.right * _dash);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0)
        {
            rgb.AddForce(gameObject.transform.forward * _dash);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Vertical") < 0)
        {
            rgb.AddForce(-gameObject.transform.forward * _dash);
        }
    }
    void jump()
    {
        if (rgb.velocity.y == 0)
        {
            canJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space)&&canJump)
        {
            rgb.AddForce(0, _jump, 0);
            canJump = false;
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
