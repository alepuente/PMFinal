using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToDungeon : MonoBehaviour {
	public DungeonStates _gameController;
	private AsyncOperation _async = null;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player") {
			StartCoroutine (ChangeScene());
		}
	}

	IEnumerator ChangeScene()
	{
			float fadeTime = gameObject.GetComponent<FadeTransition>().BeginFade(1);
			yield return new WaitForSeconds(fadeTime);
			_gameController.restartStates ();
			SceneManager.LoadScene ("ProceduralTests");
			//StartCoroutine (LoadLevel ());
			Debug.Log ("Go To Dungeon!");
	}

	private IEnumerator LoadLevel()
	{
		//yield return new WaitForSeconds(1.0f);
		_async = SceneManager.LoadSceneAsync("ProceduralTests");
		yield return _async;
		// sacar pantalla de loading
	}

}
