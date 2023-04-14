using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
 using UnityEngine.UI;
 using TMPro;
using UnityEngine.SceneManagement;

public class JoinRoom : MonoBehaviour
{
    [SerializeField]
    GameObject roomCodeInput;

    public void joinRoom() {
        string name = roomCodeInput.GetComponent<TMP_InputField>().text;
        switch (name) {
            case "small":
                SceneManager.LoadScene(6);
                break;
            case "medium":
                SceneManager.LoadScene(7);
                break;
            case "large":
                SceneManager.LoadScene(8);
                break;
            case "multiplayer":
                SceneManager.LoadScene(9);
                break;
        }
    }
    
}
