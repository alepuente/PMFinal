using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMesh))]
public class EnemyController : MonoBehaviour {

	public NavMeshAgent _nav = null;
	private Transform _target;
	public float _maxDistance = 10.0f;
	public float _health = 100f;
    private PlayerController _player;
    private Rigidbody _rgb;
    public float _exp;
    public float _attackDistance;
    public float _damage;
    public float _attackSpeed;
    private float _attackTimer;
    public DungeonStates _gameController;
	public float criticalHit;

	Animator anim;

	void Start () {
		_nav = gameObject.GetComponent<NavMeshAgent>();
		_nav.enabled = false;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		_target = GameObject.FindGameObjectWithTag("Player").transform;
        _rgb = gameObject.GetComponent<Rigidbody>();
        _damage +=_gameController._dungeonLvl * 3;

		anim = GetComponent <Animator> ();
		anim.SetBool ("IsWalking", false);
	}
    void OnDestroy()
    {
        _player._enemyKill.Invoke(_exp);
    }

	void Update (){
		
		if (_rgb.velocity.y <  criticalHit&&gameObject.transform.position.y < criticalHit)
        {
			Debug.Log ("CriticalHit");
			Destroy (gameObject);
        }
		
        attack();
		if (_health<=0)
		{
			Destroy(gameObject);
		}

		if (Vector3.Distance(gameObject.transform.position,_target.position)<= _maxDistance&&_nav.enabled)
		{
			_nav.SetDestination(_target.position);
			anim.SetBool ("IsWalking", true);
		}  else anim.SetBool ("IsWalking", false);
	}
    void attack()
    {
        _attackTimer += Time.deltaTime;
        if (Vector3.Distance(gameObject.transform.position, _target.position) <= _attackDistance && _nav.enabled && _attackTimer >= _attackSpeed)
        {
            _player._health -= _damage;
            _attackTimer = 0f;
            if (_player._health<= 0)
            {
                Destroy(_player);
            }
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
