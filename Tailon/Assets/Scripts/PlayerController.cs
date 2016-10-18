using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float _velocity;
    public float _jump;
    private Rigidbody rgb;
    void Start()
    {
        rgb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        jump();
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
