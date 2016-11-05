using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToDungeon : MonoBehaviour {
	public DungeonStates _gameController;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player") {
			_gameController.playerLevel = 0;
			_gameController.restartStates ();
			SceneManager.LoadScene ("ProceduralTests");
			Debug.Log ("Go To Dungeon!");
		}
	}

}
