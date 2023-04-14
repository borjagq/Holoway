using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameSettings 
{
    public GlobalAudioSettings AudioSettings = new GlobalAudioSettings();
    public GlobalNetworkSettings NetworkSettings = new GlobalNetworkSettings();
    public static GlobalGameSettings Instance = new GlobalGameSettings();
    public GlobalGameSettings()
    {
        Debug.Log("Loading Global Game Settings...");
    }
    public static GlobalGameSettings GetInstance()
    {
        return Instance;
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("AudioSettings_AudioVolume", AudioSettings.GetAudioVolume());
        PlayerPrefs.SetFloat("AudioSettings_MicrophoneVolume", AudioSettings.GetMicrophoneVolume());
        PlayerPrefs.SetFloat("AudioSettings_UIVolume", AudioSettings.GetUIVolume());
        PlayerPrefs.SetString("NetworkSettings_IPAddress", NetworkSettings.GetIPAddress());
        PlayerPrefs.SetInt("NetworkSettings_Port", NetworkSettings.GetPort());
    }
    public void LoadSettings()
    {
        // Load the Audio Settings
        AudioSettings.SetAudioVolume(PlayerPrefs.GetFloat("AudioSettings_AudioVolume"));
        AudioSettings.SetMicrophoneVolume(PlayerPrefs.GetFloat("AudioSettings_MicrophoneVolume"));
        AudioSettings.SetUIVolume(PlayerPrefs.GetFloat("AudioSettings_UIVolume"));
        // Load the Network Settings
        NetworkSettings.SetIPAddress(PlayerPrefs.GetString("NetworkSettings_IPAddress"));
        NetworkSettings.SetPort(PlayerPrefs.GetInt("NetworkSettings_Port"));
    }
}
