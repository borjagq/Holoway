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
    }
    public void ShowMenuItem(GameObject MenuItem)
    {
        if (!MenuItem.activeSelf)
        {
            MenuItem.SetActive(true);
        }
    }
}
