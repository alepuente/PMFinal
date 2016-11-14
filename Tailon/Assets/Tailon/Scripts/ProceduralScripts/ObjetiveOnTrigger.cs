using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ObjetiveOnTrigger : MonoBehaviour {
	public DungeonStates _gameController;

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player") {
			Debug.Log ("On TriggerEnter Player");


			_gameController.dungeonWidth += _gameController._dungeonLvl + 10;
			_gameController.dungeonHeight += _gameController._dungeonLvl + 10;
			_gameController.roomMaxSize += 1;
			_gameController.roomMinSize = 10;
			_gameController.roomMaxMonsters += 2;
			_gameController.maxRooms += 1;
			_gameController._playerLevel += 2;

			StartCoroutine (ChangeScene ());

		}
	}

		IEnumerator ChangeScene()
		{
			float fadeTime = gameObject.GetComponent<FadeTransition>().BeginFade(1);
			yield return new WaitForSeconds(fadeTime);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			Debug.Log ("Go To Dungeon!");
		}
}
