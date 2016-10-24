using UnityEngine;
using System.Collections;

public class MeleeAttackController : MonoBehaviour {

    PlayerController _playerController;
	public float _range;
    public float _knockback;
    public float _distance;
	// Use this for initialization
	void Start () {
        _playerController = GetComponentInParent<PlayerController>();
	}
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(gameObject.transform.position+(gameObject.transform.forward*_distance), _range);
    }

    void Update()
    {
        attack();
    }
    void attack()
    {        
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit[] hit;
            hit = Physics.SphereCastAll(gameObject.transform.position + (gameObject.transform.forward * _distance), _range, Vector3.forward);
            
                Debug.Log("Hit");
				for (int i = 0; i < hit.Length; i++) {				
					if (hit[i].collider.gameObject.tag == "Enemy") {
                    Debug.Log("Cast: "+gameObject.transform.position);
                    hit[i].rigidbody.AddExplosionForce(_knockback, gameObject.transform.position + (gameObject.transform.forward * _distance), _range);
					}       
				}        
        }
    }
}
