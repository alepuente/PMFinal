using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour {

    public GameObject Player;
    public GameObject MainCamera;
    public Canvas Canvas;

	void Start () {
        Instantiate(Player, transform.position, transform.rotation);
        Instantiate(MainCamera, transform.position, transform.rotation);
        Instantiate(Canvas, Vector3.zero, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
