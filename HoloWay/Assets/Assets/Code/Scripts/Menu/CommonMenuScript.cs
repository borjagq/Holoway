using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonMenuScript : MonoBehaviour
{
    [Header("Menu - Volume Controls")]
    public Slider VolumeSlider;
    void Start()
    {
        if(VolumeSlider != null)
        {
            VolumeSlider.value = GlobalGameSettings.AudioSettings.GetAudioVolume();
        }    
    }
    public void OnVolumeSliderChange()
    {
        Debug.Log("Result slider value = " + VolumeSlider.value);
    }
}
