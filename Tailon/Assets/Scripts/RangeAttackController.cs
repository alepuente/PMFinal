using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class RangeAttackController : MonoBehaviour {

    //Attack
    public GameObject _basicAttack;
    private List<GameObject> _ammo;
    public float _attackSpeed = 0.2f;
    private float timerAttack;
    public int _maxAmmo = 10;
    public bool _canGrow = false;
    PlayerController _playerController;

    void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _ammo = new List<GameObject>();
        for (int i = 0; i < _maxAmmo; i++)
        {
            GameObject obj = (GameObject)Instantiate(_basicAttack);
            obj.SetActive(false);
            _ammo.Add(obj);
        }
    }
    void Update()
    {
        if (_playerController._range)
        {           
        shoot(); 
        }
        var y = CrossPlatformInputManager.GetAxis("Mouse Y");
        _playerController._bow.transform.Rotate(0, y * 1.5f, 0);
    }
    void shoot()
    {
        timerAttack += Time.deltaTime;
        if (Input.GetMouseButton(0) && timerAttack > _attackSpeed)
        {            
            GameObject obj = getPoolObject();
            if (obj == null) return;
            obj.transform.position = _playerController._bow.transform.position;
            obj.transform.rotation = gameObject.transform.rotation;
            obj.GetComponent<Projectile>().setDamage(_playerController._rangeDamage);
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
        if (_canGrow)
        {
            GameObject obj = (GameObject)Instantiate(_basicAttack);
            _ammo.Add(obj);
            return obj;
        }

        return null;
    }

}
