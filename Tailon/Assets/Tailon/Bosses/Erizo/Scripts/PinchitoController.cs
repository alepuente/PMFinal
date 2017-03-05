using UnityEngine;
using System.Collections;

public class PinchitoController : MonoBehaviour {

    public Animator anim;
    public int damage = 20;
    private PinchitosSpawn attack1;
    public GameObject pinchito;

    public float timer = 0;
    public float timer2 = 0;
    private bool attacking = false;

    public float timeAttack1 = 10;
    public float timeAttack2 = 30;
    
    

	void Start () {
        anim.SetBool("Iddle", true);
        attack1 = pinchito.GetComponent<PinchitosSpawn>();
	}
	
	void Update () {
        timer += Time.deltaTime;

        if (timer < timeAttack1)
        {
            attack1.Attack1 = true;
        }
        else
        {
            timer2 += Time.deltaTime;
            attack1.Attack1 = false;

            if (timer2 > timeAttack1)
            {
                if (!attacking) anim.SetTrigger("Attack2");
                attacking = true;
            }
        }


        if (timer2 > timeAttack2 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2BossSphere_Anim") && attacking)
        {
            timer = 0;
            timer2 = 0;
            attacking = false;
        }
	}

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<PlayerController>()._health -= damage;

            if (hit.gameObject.GetComponent<PlayerController>()._health <= 0)
            {
                Destroy(hit.gameObject);
            }
        }
    }
}
