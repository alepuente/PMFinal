using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public NavMeshAgent _nav;
    private Transform _target;
    public float _maxDistance = 10.0f;
    public float _health = 100f;

	void Start () {
        _nav = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
	}	
	void Update () {
        if (_health<=0)
        {
            Destroy(gameObject);
        }
        if (Vector3.Distance(gameObject.transform.position,_target.position)<= _maxDistance&&_nav.enabled)
        {
            _nav.SetDestination(_target.position);
        }
	}
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "floor")
        {
            _nav.enabled = true;
        }
    }
    void OnCollisionStay(Collision hit)
    {
        if (hit.gameObject.tag == "floor")
        {
            _nav.enabled = true;
        }
    }
}
