using UnityEngine;
using System.Collections;

public class PinchitosSpawn : MonoBehaviour {

    public GameObject pinchito;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        transform.forward = player.transform.position;
    }


    void spawnPinchitos(float delay)
    {
        Invoke("spawnPinchitos", delay);
    }

    void spawnPinchitos()
    {

    }
}
