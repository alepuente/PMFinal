using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackController : MonoBehaviour {

    //Attack
    public GameObject basicAttack;
    private List<GameObject> _ammo;
    public float attackSpeed = 0.2f;
    private float timerAttack;
    public int _maxAmmo = 10;
    public bool canGrow = false;


    void Start()
    {
        _ammo = new List<GameObject>();
        for (int i = 0; i < _maxAmmo; i++)
        {
            GameObject obj = (GameObject)Instantiate(basicAttack);
            obj.SetActive(false);
            _ammo.Add(obj);
        }
    }

    void Update()
    {
        shoot();
    }
    void shoot()
    {
        timerAttack += Time.deltaTime;
        if (Input.GetMouseButton(0) && timerAttack > attackSpeed)
        {
            /*for (int i = 0; i < _ammo.Count; i++)
            {
                if (!_ammo[i].activeInHierarchy)
                {
                    _ammo[i].transform.position = gameObject.transform.position;
                    _ammo[i].transform.rotation = gameObject.transform.rotation;
                    _ammo[i].SetActive(true);
                    timerAttack = 0.0f;
                    break;
                }
            }*/
            GameObject obj = getPoolObject();
            if (obj == null) return;
            obj.transform.position = gameObject.transform.position;
            obj.transform.rotation = gameObject.transform.rotation;
            obj.SetActive(true);
            timerAttack = 0.0f;
        }
    }

    public GameObject getPoolObject()
    {
        for (int i = 0; i < _ammo.Count; i++)
        {
            if (!_ammo[i].activeInHierarchy)
            {
                return _ammo[i];
            }
        }
        if (canGrow)
        {
            GameObject obj = (GameObject)Instantiate(basicAttack);
            _ammo.Add(obj);
            return obj;
        }

        return null;
    }

}
