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
        if(MenuAudioSource == null)
        {
            MenuAudioSource = Camera.main.GetComponent<AudioSource>();
            if(MenuAudioSource == null)
            {
                MenuAudioSource = Camera.main.gameObject.AddComponent<AudioSource>();
            }
        }
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
