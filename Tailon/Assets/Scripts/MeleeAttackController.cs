using UnityEngine;
using System.Collections;

public class MeleeAttackController : MonoBehaviour
{

    PlayerController _playerController;
    public float _range;
    public float _knockback;
    public float _distance;
    private float _explosionRange = 100.0f;
    // Use this for initialization
    void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
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
            RaycastHit[] hits;
            hits = Physics.SphereCastAll(gameObject.transform.position + (gameObject.transform.forward * _distance), _range, Vector3.forward);
            foreach(RaycastHit hit in hits)
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    hit.rigidbody.AddExplosionForce(_knockback, gameObject.transform.position + (gameObject.transform.forward * _distance), _explosionRange, 0.5f);
                    _playerController._meleeAttack.Invoke(_playerController._meleeDamage);
                }
            
        }
    }
}
