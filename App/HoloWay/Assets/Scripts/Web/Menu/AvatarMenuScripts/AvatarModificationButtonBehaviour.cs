using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarModificationButtonBehaviour : MonoBehaviour
{
    public GameObject CurrentMenu;
    public GameObject NextMenu;
    public GameObject PreviousMenu;
    public void NavigateForward()
    {
        CurrentMenu.SetActive(false);
        NextMenu.SetActive(true);
    }
    public void NavigateBackwards()
    {
        CurrentMenu.SetActive(false);
        PreviousMenu.SetActive(true);
    }
}
