using UnityEngine;
using System.Collections;

public class MaterialModifier : MonoBehaviour {

    public Material mat;

    private Vector2 tmp;
    public float modifier = 0.1f;

    void Start()
    {
        tmp = Vector2.zero;
    }

    void Update()
    {
        tmp.x += modifier * Time.deltaTime;
        tmp.y += modifier * Time.deltaTime;

        mat.mainTextureOffset = tmp;
    }
}
