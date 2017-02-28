using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {
    private TextMesh tex3D;
    private Color transparent = Color.white;
    private Color color = Color.white;
    void Start()
    {
        tex3D = GetComponent<TextMesh>();
        transparent.a = 0;
        tex3D.color = transparent;
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.transform.tag == "Player")
        {
            tex3D.color = color;
        }
    }
    void OnTriggerExit(Collider hit)
    {
        if (hit.transform.tag == "Player")
        {
            tex3D.color = transparent;
;
        }
    }
}
