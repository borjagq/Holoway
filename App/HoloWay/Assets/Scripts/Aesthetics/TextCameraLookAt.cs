using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCameraLookAt : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {

        /*this.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);*/
        Vector3 InverseVector = this.transform.position - Camera.main.transform.position;
        InverseVector = Vector3.Normalize(InverseVector);
        this.transform.rotation = Quaternion.LookRotation(InverseVector, Vector3.up);
    }
}
