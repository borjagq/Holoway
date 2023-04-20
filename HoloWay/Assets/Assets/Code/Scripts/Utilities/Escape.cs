using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChangeScene;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKey ("escape")) {
            SceneManager.LoadScene(6);
        }
    }
}
