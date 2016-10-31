using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private NavMeshAgent _nav;
    private Transform _target;
    public float _maxDistance = 10.0f;
    public float health = 100f;
	void Start () {
        _nav = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>()._meleeAttack.AddListener(applyDamage);
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(gameObject.transform.position,_target.position)<= _maxDistance )
        {
            _nav.SetDestination(_target.position);
        }          
	}
    void applyDamage(float damage)
    {
        health -= damage;
    }
}
