using UnityEngine;
using System.Collections;

public class SpawnObjectiveTP : MonoBehaviour {
    
    public GameObject tp;

    public void spawnObjectiveTP()
    {
        Instantiate(tp, transform.position, transform.rotation);
    }
}
