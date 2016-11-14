using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    public Texture2D fadeOutTexture;
    public float fadeSpeed;

    private int _drawDepth = -1000;
    private float _alpha = 1.0f;
    private int _fadeDirection = -1; // in -> -1, out -> 1


    void OnGUI()
    {
        _alpha += _fadeDirection * fadeSpeed * Time.deltaTime;
        _alpha = Mathf.Clamp01(_alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, _alpha);
        GUI.depth = _drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        _fadeDirection = direction;
        return fadeSpeed;
    }

	void SceneLoaded(Scene scene, LoadSceneMode mode)
	{
		BeginFade(-1);
	}
}
