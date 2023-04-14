using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMenuUtilities : MonoBehaviour
{
    public void HideMenuItem(GameObject MenuItem)
    {
        if(MenuItem.activeSelf)
        {
            MenuItem.SetActive(false);
        }
        else Debug.LogError($"Menu object ${MenuItem.name} is already hidden!");
    }
    public void ShowMenuItem(GameObject MenuItem)
    {
        if (!MenuItem.activeSelf)
        {
            MenuItem.SetActive(true);
        }
        else Debug.LogError($"Menu object ${MenuItem.name} is already visible!");
    }
}
