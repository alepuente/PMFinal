using UnityEngine;
using System.Collections;

public class PinchitoCollider : MonoBehaviour
{

    public int damage = 50;
    public int speed = 50;

    private Rigidbody rgb;
    public float LifeTime = 10;
    private float tmp = 0;

    public bool forward;

    void Start()
    {
        rgb = gameObject.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<PlayerController>()._health -= damage;

            if (hit.gameObject.GetComponent<PlayerController>()._health <= 0)
            {
                Destroy(hit.gameObject);
            }

            Destroy(gameObject);
        }

    }

    void Update()
    {
        if(forward)
            rgb.AddForce(-transform.forward * speed);
        else
            rgb.AddForce(transform.forward * speed);


        tmp += Time.deltaTime;
        if (tmp >= LifeTime)
            Destroy(gameObject);
    }
}
