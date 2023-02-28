using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
/*using UnityEngine.UI;*/
public class RoomCreationMenuScript : MonoBehaviour
{
    public UnityEngine.UI.Button ConfirmButton;
    public TMP_InputField RoomCodeInput;
    public void BackButtonOnClick()
    {
        SceneManager.LoadScene(1);
    }
    public void CreateRoomButtonOnClick()
    {
        SceneManager.LoadScene(3);
    }
    public void RoomCodeOnInput()
    {
        if(RoomCodeInput.text.Length > 0)
            ConfirmButton.interactable = true;
        else
            ConfirmButton.interactable= false;
    }

}
