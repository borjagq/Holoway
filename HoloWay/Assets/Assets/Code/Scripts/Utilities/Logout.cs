using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Logout : MonoBehaviour
{
    public void LogoutFunction()
    {
        PlayerPrefs.DeleteKey("player_token");
        SceneManager.LoadScene(0);
    }
    public void DeletePlayerTokenAndLogout()
    {
        PlayerPrefs.DeleteKey("player_token");
        //SceneManager.LoadScene(0);
    }
}
