using UnityEngine;
using System.Collections;

public class WallTilesPerformance : MonoBehaviour {
	
	void OnCollitionStay(Collider hit){
		
		if (hit.gameObject.tag != "floor") {
			Destroy (gameObject);
			print ("FloorWALL");
		}

	}
}
