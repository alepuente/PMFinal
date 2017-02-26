using UnityEngine;
using System.Collections;

public class EnemyImpactSpawner : MonoBehaviour {

    public GameObject[] spawners;

    private float _tmp = 0;

    void Start()
    {
        int tmp = Random.Range(0, spawners.Length);
        bool ready = false;

        while (!ready)
        {
            int tmp2 = Random.Range(0, spawners.Length);
            if (spawners[tmp2].GetComponent<MobSpawner>().canSpawn == false)
            {
                spawners[tmp2].GetComponent<Animator>().SetTrigger("Spawn");
                spawners[tmp2].GetComponent<MobSpawner>().canSpawn = true;
            }

            --tmp;
            if (tmp <= 0) ready = true;
        }
    }

    void Update()
    {
        _tmp += Time.deltaTime;

        if (_tmp > 5)
        {
            Destroy(gameObject);
        }
    }
}
