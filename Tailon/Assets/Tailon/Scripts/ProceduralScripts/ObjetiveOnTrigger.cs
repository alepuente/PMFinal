using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ObjetiveOnTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player") {
			Debug.Log ("On TriggerEnter Player");

            if (DungeonStates.instance._dungeonLvl == 10)
            {
                SceneManager.LoadScene("GameOver");
            }

            DungeonStates.instance._dungeonLvl++;
			StartCoroutine (ChangeScene ());
		}
	}

		IEnumerator ChangeScene()
		{
			float fadeTime = gameObject.GetComponent<FadeTransition>().BeginFade(1);
			yield return new WaitForSeconds(fadeTime);

            switch (DungeonStates.instance._dungeonLvl)
            {
                case 3:
                    {
                        SceneManager.LoadScene("Boss3");
                        Debug.Log("Go To BOSS!");
                    }
                    break;

                case 6:
                    {
                        SceneManager.LoadScene("Boss1");
                        Debug.Log("Go To BOSS!");
                    }
                    break;

                case 10:
                    {
                        SceneManager.LoadScene("Boss2");
                        Debug.Log("Go To BOSS!");
                    }
                    break;

                default:
                    {
                        DungeonStates.instance.dungeonWidth += DungeonStates.instance._dungeonLvl + 10;
                        DungeonStates.instance.dungeonHeight += DungeonStates.instance._dungeonLvl + 10;
                        DungeonStates.instance.roomMaxSize += 1;
                        DungeonStates.instance.roomMinSize = 10;
                        DungeonStates.instance.roomMaxMonsters += 2;
                        DungeonStates.instance.maxRooms += 1;
                        SceneManager.LoadScene("ProceduralTests");
                        Debug.Log(SceneManager.GetActiveScene().name);
                    }
                    break;
            }

		}
}
