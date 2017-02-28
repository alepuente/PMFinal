using UnityEngine;
using System.Collections;

public class FireBallImpact : MonoBehaviour {
    
    public GameObject impact;
    public int damage = 50;

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "floor")
        {
            Vector3 tmp = Vector3.zero;
            tmp.x = transform.position.x;
            tmp.y = 0.1f;
            tmp.z = transform.position.z;

            Instantiate(impact, tmp, transform.rotation);
            gameObject.SetActive(false);
        }
        else
            if (hit.gameObject.tag == "Player")
        {
            Vector3 tmp = Vector3.zero;
            tmp.x = transform.position.x;
            tmp.y = 0.1f;
            tmp.z = transform.position.z;

            Instantiate(impact, tmp, transform.rotation);
            gameObject.SetActive(false);

            hit.gameObject.GetComponent<PlayerController>()._health -= damage;
        }
            else
                if (hit.gameObject.tag == "Enemy")
                {
                    Vector3 tmp = Vector3.zero;
                    tmp.x = transform.position.x;
                    tmp.y = 0.1f;
                    tmp.z = transform.position.z;

                    Instantiate(impact, tmp, transform.rotation);
                    gameObject.SetActive(false);
                }

    }

}
