using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public AudioClip ButtonClickAudioClip;
    public AudioClip ButtonHoverAudioClip;
    public AudioSource MenuAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDemo_OnClick()
    {
        DoButtonDefaults(0);
        SceneManager.LoadScene(5);
    }
    public void Settings_OnClick()
    {
        DoButtonDefaults(0);
        SceneManager.LoadScene(4);
    }
    public void Avatar_OnClick()
    {
        DoButtonDefaults(0);
        SceneManager.LoadScene(6);
    }
    public void BrowseRooms_OnClick()
    {
        DoButtonDefaults(0);
        SceneManager.LoadScene(GlobalGameSettings.SCENE_INDEX_ROOMCREATIONMENU);
    }
    public void Logout_OnClick()
    {
        DoButtonDefaults(0);
        SceneManager.LoadScene(0);
    }
    public void Quit_OnClick()
    {
        DoButtonDefaults(0);
        Application.Quit();
    }
    public void OnButtonClick()
    {
        DoButtonDefaults(0);
    }
    public void OnButtonHover()
    {
        DoButtonDefaults(1); 
    }
    

    public void DoButtonDefaults(int type)
    {
        switch(type)
        {
            case 0:             //Button was clicked
            {
                MenuAudioSource.PlayOneShot(ButtonClickAudioClip);
                break;
            }
            case 1:             //Button was hovered
            {
                MenuAudioSource.PlayOneShot(ButtonHoverAudioClip);
                break;
            }
        }
    }
}
