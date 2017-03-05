using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BossHealth : MonoBehaviour
{

    public float health = 500;
    public float healthMax = 500;

    private Image _healthBar;

    void Start()
    {
        _healthBar = GameObject.FindGameObjectWithTag("BossCanvas").GetComponent<HUDVolcan>().bossHealthBar;
    }
	

	void Update () {

        _healthBar.fillAmount = health / healthMax;

        if (health <= 0)
        {
            gameObject.GetComponent<SpawnObjectiveTP>().spawnObjectiveTP();
            Debug.Log("<color=yellow>BOSS DEATH!</color>");
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            health -= 10;
        }
	}
}
