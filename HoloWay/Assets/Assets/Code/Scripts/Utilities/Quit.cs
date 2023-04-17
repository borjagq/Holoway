using UnityEngine;
using System.Collections;

// Quits the player when the user hits escape

public class Quit : MonoBehaviour
{
    public void ReturnToDescktop()
    {
        PlayerPrefs.DeleteKey("player_token");
        Application.Quit();
    }
}