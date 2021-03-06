﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float _speed;
    public float _damage;

    void OnEnable()
    {
        Invoke("Disable", 0.5f);
        _damage = DungeonStates.instance._rangeDamage;
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
        if (hit.gameObject.tag == "Enemy2")
        {
            hit.gameObject.GetComponent<EnemyHealth>()._hitEmitter.Play();
            hit.gameObject.GetComponent<EnemyHealth>()._health -= _damage;
            gameObject.SetActive(false);
        }
        else if (hit.gameObject.tag == "Boss")
        {
            hit.gameObject.GetComponent<BossHealth>().health -= _damage;
            gameObject.SetActive(false);
        }
        else if (hit.gameObject.tag == "wall")
        {
            gameObject.SetActive(false);
        }
    }
    public void setDamage(float damage)
    {
        _damage = damage;
    }
	void Update () {                   
        gameObject.transform.Translate(Vector3.forward * _speed*Time.deltaTime); 
	}   
}
