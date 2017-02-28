using UnityEngine;
using System.Collections;

public class ControllerPelotudo : MonoBehaviour {

    public float rotVelocity = 50;

    void Update () {

        transform.Rotate(Vector3.up, rotVelocity * Time.deltaTime);

    }
    
}
