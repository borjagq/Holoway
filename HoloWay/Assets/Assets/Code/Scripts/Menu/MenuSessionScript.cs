using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuSessionScript : MonoBehaviour
{
    public TMP_Text MainMenuLoggedInNameText;
    // Start is called before the first frame update
    void Start()
    {
        if(MainMenuLoggedInNameText != null)
        {
            string name = GlobalGameSettings.Instance.SessionSettings.GetSessionName();
            if(name != null)
            {
                if(name != "" && name.Length > 0)
                {
                    MainMenuLoggedInNameText.text = "Logged in as " + name;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
