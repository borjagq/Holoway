using UnityEngine;
using System.Collections;

// Quits the player when the user hits escape

public class Quit : MonoBehaviour
{
    public void ReturnToDescktop()
    {
        Application.Quit();
    }
}