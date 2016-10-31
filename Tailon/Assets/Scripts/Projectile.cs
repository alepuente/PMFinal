using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float _speed;
    void OnEnable()
    {
        Invoke("Destroy", 2f);
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        CancelInvoke();
    }

    void OnCollisionEnter(Collision hit)
    {
        /*if (hit.gameObject.tag == "Enemy")
        {
           _playerController._meleeAttack.Invoke(_playerController._meleeDamage);
        }*/
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(Vector3.forward * _speed);
	}

    
}
