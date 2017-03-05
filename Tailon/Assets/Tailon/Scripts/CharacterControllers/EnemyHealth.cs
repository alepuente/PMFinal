using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public float _health = 100;
    public ParticleSystem _hitEmitter;
    public float _exp;
    public int _money;

    public PlayerController _player;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

	void Update () {

        if (_health <= 0)
        {
            gameObject.SetActive(false);
            _player._enemyKill.Invoke(_exp, _money);
        }
	}
}
