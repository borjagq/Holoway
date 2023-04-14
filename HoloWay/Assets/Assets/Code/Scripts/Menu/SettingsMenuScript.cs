using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    [Header("Menu - Volume Controls")]
    public Slider VolumeSlider;
    public Slider MicrophoneVolumeSlider;
    public Slider UIVolumeSlider;
    [Header("Menu - Network Controls")]
    public TMP_InputField IPAddressField;
    public TMP_InputField PortField;
    void Start()
    {
        GlobalGameSettings.Instance.LoadSettings();
        if (VolumeSlider != null)
            VolumeSlider.value = GlobalGameSettings.Instance.AudioSettings.GetAudioVolume();
        if (MicrophoneVolumeSlider != null)
            MicrophoneVolumeSlider.value = GlobalGameSettings.Instance.AudioSettings.GetMicrophoneVolume();
        if (UIVolumeSlider != null)
            UIVolumeSlider.value = GlobalGameSettings.Instance.AudioSettings.GetUIVolume();
        if (IPAddressField != null)
            IPAddressField.text = GlobalGameSettings.Instance.NetworkSettings.GetIPAddress();
        if (PortField != null)
            PortField.text = GlobalGameSettings.Instance.NetworkSettings.GetPort().ToString();

    }
    public void OnMicrophoneVolumeChange()
    {
        GlobalGameSettings.Instance.AudioSettings.SetMicrophoneVolume(MicrophoneVolumeSlider.value);
    }
    public void OnUIVolumeChange()
    {
        GlobalGameSettings.Instance.AudioSettings.SetUIVolume(UIVolumeSlider.value);
    }
    public void OnVolumeSliderChange()
    {
        GlobalGameSettings.Instance.AudioSettings.SetAudioVolume(VolumeSlider.value);
    }
    public void IPAddressField_OnChange()
    {
        GlobalGameSettings.Instance.NetworkSettings.SetIPAddress(IPAddressField.text);
    }
    public void PortField_OnChange()
    {
        GlobalGameSettings.Instance.NetworkSettings.SetPort(Int32.Parse(PortField.text));
    }
    public void SettingsOnSave()
    {
        GlobalGameSettings.Instance.SaveSettings();
    }
}
