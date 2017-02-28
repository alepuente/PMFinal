using UnityEngine;
using System.Collections;

public class PinchitoCollider : MonoBehaviour
{

    public int damage = 50;
    public int speed = 50;

    private Rigidbody rgb;
    public float LifeTime;
    private float tmp = 0;


    void Start()
    {

        rgb = gameObject.GetComponent<Rigidbody>();

    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<PlayerController>()._health -= damage;
            Destroy(gameObject);
        }

    }

    void Update()
    {
        rgb.AddForce(transform.forward * speed);

        tmp += Time.deltaTime;
        if (tmp >= LifeTime)
            Destroy(gameObject);
    }
}
