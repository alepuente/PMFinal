using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeleeAttackController : MonoBehaviour
{

    PlayerController _playerController;
    private EnemyController _enemy;
    public float _range;
    public float _knockback;
    public float _height;
    public float _distance;

    private GameObject _sword;
    private Animator _swordAnim;
    
    private Image _stamineBar;
    public float stamine = 100;
    public float maxStamine = 100;
    public float staminenNeeded = 30;

    void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _sword = GameObject.Find("Sword");
        _swordAnim = _sword.GetComponent<Animator>();
        _stamineBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<hUDScript>()._stamineBar;
        stamine = DungeonStates.instance._maxStamine;
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

        if (stamine < DungeonStates.instance._maxStamine) stamine += DungeonStates.instance._staminaRestorage * Time.deltaTime;
        _stamineBar.fillAmount = stamine / maxStamine;

        if (_playerController.onStaminaPot)
        {
            if (stamine < DungeonStates.instance._maxStamine)
                 stamine += (DungeonStates.instance._staminaRestorage *2) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.C)) stamine = 0;
    }
    void attack()
    {
        if (Input.GetMouseButtonDown(0) && stamine > staminenNeeded && !_swordAnim.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack"))
        {
            stamine -= staminenNeeded;
            Collider[] hits;
            hits = Physics.OverlapSphere(gameObject.transform.position + (gameObject.transform.forward * _distance), _range);
            _swordAnim.SetTrigger("SwordAttack");
            foreach (Collider hit in hits)
            {
                if (hit.gameObject.tag == "Enemy")
                {
                    _enemy = hit.GetComponent<EnemyController>();
                    _enemy._isTouching = false;
                    _enemy._stateMachine.CurrentState = new Iddle();
                    _enemy._health -= _playerController._meleeDamage;
                    _enemy._hitEmitter.Play();
                    hit.GetComponent<Rigidbody>().AddForce((gameObject.transform.forward.x * _knockback), Mathf.Abs(_enemy.transform.up.y * _height), (gameObject.transform.forward.z * _knockback));
                }
            }
        }
    }
}
