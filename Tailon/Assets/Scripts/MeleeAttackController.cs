using UnityEngine;
using System.Collections;

public class MeleeAttackController : MonoBehaviour {

    PlayerController _playerController;
    public Vector3 _distance;
    public int _range;
    public bool _hit;
    public float _knockback = 1;
	// Use this for initialization
	void Start () {
        _playerController = GetComponentInParent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_playerController._melee)
        {
            attack();
        }
	}

    void attack()
    {        
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.SphereCast(_playerController.transform.position + _distance, _range, transform.forward, out hit))
            {
                Debug.Log("Hit");
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    Debug.Log("Hit Enemy");
                    hit.rigidbody.AddForce(Vector3.back);
                }  
                
            }
            else
            {
                Debug.Log("No hit");
            }
        }
    }
}
