using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Transform _target;
	public float _maxDistance = 10.0f;
	public float _health = 100f;
    public PlayerController _player;
    public float _speed;
    public float _exp;
    public float _attackDistance;
    public float _damage;
    public float _attackSpeed;
    private float _attackTimer;
    public DungeonStates _gameController;
	public float criticalHit;
    public ParticleSystem _hitEmitter;
    public bool _canAttack = true;
	public Rigidbody _rgb;
    public bool _isTouching;
	public StateMachine<EnemyController> _stateMachine;
	public Animator anim;

	void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
		_target = GameObject.FindGameObjectWithTag ("Player").transform;
		_damage += _gameController._dungeonLvl * 3;
		anim = GetComponent <Animator> ();
		anim.SetBool ("IsWalking", false);
		_rgb = gameObject.GetComponent<Rigidbody> ();
		_stateMachine = new StateMachine<EnemyController> (new WanderAround (), this);
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
		_stateMachine.Update ();
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
            _isTouching = true;
		}
	}

	void OnCollisionStay(Collision hit)
	{
		if (hit.gameObject.tag == "floor") {
            _isTouching = true; 
		}
	}

    public void destroy()
    {
        StartCoroutine(wait(2.0f));
    }

    IEnumerator wait(float time)
    {
        _isTouching = false;
        _canAttack = false;
        _rgb.isKinematic = true;
        _rgb.isKinematic = false;
        anim.SetBool("DeadF", true);
        yield return new WaitForSeconds(time);
        _player._enemyKill.Invoke(_exp);
        Destroy(gameObject);
    }

}
