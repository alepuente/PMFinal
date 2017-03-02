using UnityEngine;
using System.Collections;

public class PelotasSpawner : MonoBehaviour {

    public GameObject pelota;
    public GameObject pelota2;

    public float spawn = 0.1f;
    private float timer = 0;
    	
	void Update () {
        timer += Time.deltaTime;

        if(timer > spawn)
        {
            Instantiate(pelota, transform.position, transform.rotation);            
            Instantiate(pelota2, transform.position, transform.rotation);
            timer = 0;
        }
	}
}
