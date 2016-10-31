using UnityEngine;
using System.Collections;

public class MeleeAttackController : MonoBehaviour
{

    PlayerController _playerController;
    public float _range;
    public float _knockback;
    public float _height;
    public float _distance;
    private float _explosionRange = 100.0f;
    private EnemyController enemy;
    // Use this for initialization
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
            foreach(Collider hit in hits)
                if (hit.gameObject.tag == "Enemy")
                {
                    Debug.Log("Hit Enemy");
                    enemy = hit.GetComponent<EnemyController>();
                    enemy._nav.enabled = false;
                    enemy.health -= _playerController._meleeDamage;
                    hit.GetComponent<Rigidbody>().AddForce((-enemy.transform.forward * _knockback) + (enemy.transform.up * _height));
                }
            
        }
    }
}
