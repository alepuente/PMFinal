using UnityEngine;
using System.Collections;

public class ObjetiveOnTrigger : MonoBehaviour {
	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player") {
			Debug.Log("On TriggerEnter Player");
		}
	}
}
