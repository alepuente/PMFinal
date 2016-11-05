using UnityEngine;
using System.Collections;

public class DonDestroyOnLoad : MonoBehaviour {
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
