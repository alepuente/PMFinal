﻿using UnityEngine;
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
    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            hit.gameObject.GetComponent<EnemyController>()._health -= _damage;
            hit.gameObject.GetComponent<EnemyController>()._hitEmitter.Play();
            gameObject.SetActive(false);
        }
    }
    public void setDamage(float damage)
    {
        _damage = damage;
    }
	void Update () {                   
        gameObject.transform.Translate(Vector3.forward * _speed); 
	}   
}