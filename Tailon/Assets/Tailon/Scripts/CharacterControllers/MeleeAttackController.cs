using UnityEngine;
using System.Collections;

public class MeleeAttackController : MonoBehaviour
{

    PlayerController _playerController;
    public float _range;
    public float _knockback;
    public float _height;
    public float _distance;
    private EnemyController _enemy;

    void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gameObject.transform.position + (gameObject.transform.forward * _distance), _range);
    }
    void Update()
    {
        if (_playerController._melee)
        {
            attack();
        }
    }
    void attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] hits;
            hits = Physics.OverlapSphere(gameObject.transform.position + (gameObject.transform.forward * _distance), _range);
            foreach (Collider hit in hits)
            {
                if (hit.gameObject.tag == "Enemy")
                {
                    _enemy = hit.GetComponent<EnemyController>();
                    _enemy._isTouching = false;
                    _enemy._stateMachine.CurrentState = new Iddle();
                    _enemy._health -= _playerController._meleeDamage;
                    _enemy._hitEmitter.Play();
                    hit.GetComponent<Rigidbody>().AddForce((gameObject.transform.forward.x * _knockback), Mathf.Abs(_enemy.transform.up.y * _height), (gameObject.transform.forward.z * _knockback));
                }
            }
        }
    }
}
