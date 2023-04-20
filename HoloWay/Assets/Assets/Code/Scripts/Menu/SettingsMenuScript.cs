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
    [Header("Menu - InGame Controls")]
    public Slider MouseSensitivityXSlider;
    public TMP_Text MouseSensitivityXPreviewText;
    public Slider MouseSensitivityYSlider;
    public TMP_Text MouseSensitivityYPreviewText;
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
        if (MouseSensitivityXSlider != null)
            MouseSensitivityXSlider.value = GlobalGameSettings.Instance.ControlSettings.MouseSensitivityX;
        if (MouseSensitivityYSlider != null)
            MouseSensitivityYSlider.value = GlobalGameSettings.Instance.ControlSettings.MouseSensitivityY;

    }
    public void OnMouseSensitivityXChange()
    {
        MouseSensitivityXPreviewText.text = MouseSensitivityXSlider.value.ToString();
        GlobalGameSettings.Instance.ControlSettings.MouseSensitivityX = MouseSensitivityXSlider.value;
    }
    public void OnMouseSensitivityYChange()
    {
        MouseSensitivityYPreviewText.text = MouseSensitivityYSlider.value.ToString();
        GlobalGameSettings.Instance.ControlSettings.MouseSensitivityY = MouseSensitivityYSlider.value;
    }
    public void OnMicrophoneVolumeChange()
    {
        GlobalGameSettings.Instance.AudioSettings.SetMicrophoneVolume(MicrophoneVolumeSlider.value);
        GlobalGameSettings.Instance.ChangeVolumeAccordingToState();
    }
    public void OnUIVolumeChange()
    {
        GlobalGameSettings.Instance.AudioSettings.SetUIVolume(UIVolumeSlider.value);
        GlobalGameSettings.Instance.ChangeVolumeAccordingToState();
    }
    public void OnVolumeSliderChange()
    {
        GlobalGameSettings.Instance.AudioSettings.SetAudioVolume(VolumeSlider.value);
        GlobalGameSettings.Instance.ChangeVolumeAccordingToState();
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
