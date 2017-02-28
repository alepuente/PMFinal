using UnityEngine;
using System.Collections;

public class LimitMap : MonoBehaviour {

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<PlayerController>()._health -= 10000;
        }
    }
}
