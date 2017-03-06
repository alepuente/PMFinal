using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TutorialEnd : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("tutorial", 1);
            PlayerPrefs.Save();
            DungeonStates.instance._dungeonLvl++;
            StartCoroutine(ChangeScene());
        }
    }

    IEnumerator ChangeScene()
    {
        float fadeTime = gameObject.GetComponent<FadeTransition>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Lobby");
    }
}
