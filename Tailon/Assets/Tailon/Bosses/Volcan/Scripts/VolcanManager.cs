using UnityEngine;

public class VolcanManager : MonoBehaviour {

    public GameObject[] Zone1;
    public GameObject[] Zone2;
    public GameObject[] Zone3;
    public GameObject[] Zone4;

    public ParticleSystem particles;

    public float attackTime = 10;
    private float _attackTimeCount = 0;
    private bool spawn = false;
    
    void Update()
    {
         _attackTimeCount += Time.deltaTime;
        if (_attackTimeCount > attackTime)
        {
            particles.Play();
            spawn = true;
            _attackTimeCount = 0;
        }

        if (spawn && !particles.isPlaying)
        {
            Attack();
            spawn = false;
        }
    }

    void Attack()
    {
        int tmp1 = Random.Range(0, Zone1.Length);
        int tmp2 = Random.Range(0, Zone2.Length);
        int tmp3 = Random.Range(0, Zone3.Length);
        int tmp4 = Random.Range(0, Zone4.Length);

        Zone1[tmp1].GetComponent<SpawnFireBall>().spawn(Random.Range(0, 1f));
        Zone2[tmp2].GetComponent<SpawnFireBall>().spawn(Random.Range(0, 1f));
        Zone3[tmp3].GetComponent<SpawnFireBall>().spawn(Random.Range(0, 1f));
        Zone4[tmp4].GetComponent<SpawnFireBall>().spawn(Random.Range(0, 1f));
    }
}