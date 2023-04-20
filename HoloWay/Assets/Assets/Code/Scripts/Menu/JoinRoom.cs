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
                GlobalGameSettings.Instance.NetworkSettings.SetNetworkType(NetworkType.Client);
                SceneManager.LoadScene(7);
                break;
            case "medium":
                GlobalGameSettings.Instance.NetworkSettings.SetNetworkType(NetworkType.Client);
                SceneManager.LoadScene(8);
                break;
            case "large":
                GlobalGameSettings.Instance.NetworkSettings.SetNetworkType(NetworkType.Client);
                SceneManager.LoadScene(9);
                break;
        }
    }
    
}
