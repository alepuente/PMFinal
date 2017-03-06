using UnityEngine;
using System.Collections;

public class Enemy4Controller : MonoBehaviour {

    public GameObject player;
	public GameObject bullet;
    

    public float timerShoot = 2;
    public float minDistance = 10;
    public float timer = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

	void Update () {

        transform.LookAt(player.transform);

        timer += Time.deltaTime;

        if (timer > timerShoot && Vector3.Distance(gameObject.transform.position, player.transform.position) < minDistance)
        {
            shoot();
            timer = 0;
        }
    }

    void shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
