using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ObjetiveOnTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player") {
			Debug.Log ("On TriggerEnter Player");

            if (DungeonStates.instance._dungeonLvl == 8)
            {
                SceneManager.LoadScene("GameOver");
            }

            DungeonStates.instance._dungeonLvl++;
            DungeonStates.instance.dungeonWidth += DungeonStates.instance._dungeonLvl + 10;
            DungeonStates.instance.dungeonHeight += DungeonStates.instance._dungeonLvl + 10;
            DungeonStates.instance.roomMaxSize += 1;
            DungeonStates.instance.roomMinSize = 10;
            DungeonStates.instance.roomMaxMonsters += 2;
            DungeonStates.instance.maxRooms += 1;
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
