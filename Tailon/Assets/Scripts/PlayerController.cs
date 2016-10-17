using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float velocity;
	public float rotation;
	void Start () {

	}

	// Update is called once per frame
	void Update () { 

		if (Input.GetAxis ("Horizontal") < 0) {
			gameObject.transform.Translate (Vector3.left * velocity);
		} 
		if (Input.GetAxis ("Horizontal") > 0){
			gameObject.transform.Translate (Vector3.right * velocity);	
		}

		if (Input.GetAxis("Vertical")> 0) {
			gameObject.transform.Translate (Vector3.forward * velocity);
		} 

		if (Input.GetAxis ("Vertical") < 0){
			gameObject.transform.Translate (Vector3.back * velocity);	
		}
	}
}
