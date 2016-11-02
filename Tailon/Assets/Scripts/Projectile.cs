using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float _speed;
    public float _damage;

    void OnEnable()
    {
        Invoke("Disable", 2f);
    }
    void Disable()
    {
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        CancelInvoke();
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            hit.gameObject.GetComponent<EnemyController>().health -= _damage;
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {                   
        gameObject.transform.Translate(Vector3.forward * _speed); 
	}

    
}
