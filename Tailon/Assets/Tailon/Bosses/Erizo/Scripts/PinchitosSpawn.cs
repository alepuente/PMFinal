using UnityEngine;
using System.Collections;

public class PinchitosSpawn : MonoBehaviour {

    public GameObject pinchito;

    private GameObject player;

    public int timeAttack = 5;
    public float attackTimer = 0;

    public bool Attack1 = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (Attack1)
        {
            transform.LookAt(player.transform.position);
            attackTimer += Time.deltaTime;
            if (attackTimer > timeAttack)
            {
                for (int i = 0; i < 20; i++)
                {
                    spawnPinchitos(Random.Range(0f, 2f));
                }

                attackTimer = 0;
            }
        }

    }


    void spawnPinchitos(float delay)
    {
        Invoke("spawnPinchitos", delay);
    }

    void spawnPinchitos()
    {
        Quaternion quat = transform.rotation;
        quat.y += Random.Range(-0.5f, 0.5f);
        quat.z += Random.Range(-0.005f, 0.005f);

        Instantiate(pinchito, transform.position, quat);
        Debug.LogError("<color=yellow>Pinchito spawneado!!</color>");
    }
}
