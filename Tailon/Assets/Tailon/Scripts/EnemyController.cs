using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMesh))]
public class EnemyController : MonoBehaviour {

	//public NavMeshAgent _nav = null;
	private Transform _target;
	public float _maxDistance = 10.0f;
	public float _health = 100f;
    private PlayerController _player;
    public float _speed;
    public float _exp;
    public float _attackDistance;
    public float _damage;
    public float _attackSpeed;
    private float _attackTimer;
    public DungeonStates _gameController;
	public float criticalHit;
    public ParticleSystem _hitEmitter;
    private bool _canAttack = true;
	private Rigidbody _rgb;
    public bool _isTouching;

	Animator anim;

	void Start () {
		//_nav = gameObject.GetComponent<NavMeshAgent> ();
		//_nav.enabled = false;
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
		_target = GameObject.FindGameObjectWithTag ("Player").transform;
		_damage += _gameController._dungeonLvl * 3;
		anim = GetComponent <Animator> ();
		anim.SetBool ("IsWalking", false);
		_rgb = gameObject.GetComponent<Rigidbody> ();
	}
    void OnDestroy()
    {
        _player._enemyKill.Invoke(_exp);
    }
    IEnumerator wait(float time)
    {
        //_nav.enabled = false;
        _isTouching = false;
        _canAttack = false;
		_rgb.isKinematic = true;
        anim.SetBool("DeadF", true);
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

	void Update (){
		if (gameObject.transform.position.y < criticalHit)
        {
			Debug.Log ("CriticalHit");
			Destroy (gameObject);
        }

        if (_canAttack)
        {            
        attack();
        }
		if (_health<=0)
		{
            StartCoroutine(wait(2.0f));
		}


		if (Vector3.Distance(gameObject.transform.position,_target.position)<= _maxDistance&&_isTouching)
		{
            _rgb.isKinematic = true;
            _rgb.isKinematic = false;
            anim.SetBool("IsWalking", true);
            gameObject.transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            gameObject.transform.LookAt(_target);
			//_nav.SetDestination(_target.position);
		}  
        else anim.SetBool ("IsWalking", false);
	}
    void attack()
    {
        _attackTimer += Time.deltaTime;
        if (Vector3.Distance(gameObject.transform.position, _target.position) <= _attackDistance && _attackTimer >= _attackSpeed)
        {
            _player._health -= _damage;
            _player._hitEmitter.Play();
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
			//_nav.enabled = true;
            _isTouching = true;
		}
	}
	void OnCollisionStay(Collision hit)
	{
		if (hit.gameObject.tag == "floor") {
            _isTouching = true; 
			// _nav.enabled = true;
		}
	}
}
