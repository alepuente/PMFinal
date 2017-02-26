using UnityEngine;
using System.Collections;

public class SpawnFireBall : MonoBehaviour {

    public GameObject fireBall;

    public void spawn(float delay)
    {
        Invoke("spawn", delay);
    }

    public void spawn()
    {
        Instantiate(fireBall, transform.position, transform.rotation);
    }
}
