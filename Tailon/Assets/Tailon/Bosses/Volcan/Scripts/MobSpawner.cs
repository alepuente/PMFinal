using UnityEngine;
using System.Collections;

public class MobSpawner : MonoBehaviour {

    public Animator anim;
    public GameObject[] enemy;
    public bool canSpawn = false;

    private float _tmp;
    public bool trigger = false;

    void Update()
    {
        _tmp += Time.deltaTime;

        if (trigger)
        {
            anim.SetTrigger("Spawn");
            trigger = false;
        }

        if (_tmp > 2.5f)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("ImpactSpawn_ANIM"))
            {
                if (canSpawn) Instantiate(enemy[Random.Range(0, enemy.Length)], transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
        }
   }
}
