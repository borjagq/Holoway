using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GlobalGameSettings 
{
    public GlobalAudioSettings AudioSettings = new GlobalAudioSettings();
    public GlobalNetworkSettings NetworkSettings = new GlobalNetworkSettings();
    public GlobalGameStates GameState = new GlobalGameStates();
    public GlobalControlSettings ControlSettings = new GlobalControlSettings();
    public GlobalSessionSettings SessionSettings = new GlobalSessionSettings();

    public static GlobalGameSettings Instance = new GlobalGameSettings();
    public GlobalGameSettings()
    {
        Debug.Log("Loading Global Game Settings...");
        
        this.LoadSettings();
        
    }
    public static void CreateInstance()
    {
        GlobalGameSettings.Instance = new GlobalGameSettings();
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
        PlayerPrefs.SetFloat("ControlSettings_MouseSensitivityX", ControlSettings.MouseSensitivityX);
        PlayerPrefs.SetFloat("ControlSettings_MouseSensitivityY", ControlSettings.MouseSensitivityY);
        PlayerPrefs.SetString("NetworkSettings_IPAddress", NetworkSettings.GetIPAddress());
        PlayerPrefs.SetInt("NetworkSettings_Port", NetworkSettings.GetPort());
        PlayerPrefs.Save();
    }
    public void LoadSettings()
    {
        Debug.Log("Loading settings called..");
        // Load the Audio Settings
        AudioSettings.SetAudioVolume(PlayerPrefs.GetFloat("AudioSettings_AudioVolume"));
        AudioSettings.SetMicrophoneVolume(PlayerPrefs.GetFloat("AudioSettings_MicrophoneVolume"));
        AudioSettings.SetUIVolume(PlayerPrefs.GetFloat("AudioSettings_UIVolume"));
        this.GameState.SetGameState(global::GameState.InMenu);
        this.ChangeVolumeAccordingToState();
        ControlSettings.MouseSensitivityX = PlayerPrefs.GetFloat("ControlSettings_MouseSensitivityX");
        ControlSettings.MouseSensitivityY = PlayerPrefs.GetFloat("ControlSettings_MouseSensitivityY");
        // Load the Network Settings
        NetworkSettings.SetIPAddress(PlayerPrefs.GetString("NetworkSettings_IPAddress"));
        NetworkSettings.SetPort(PlayerPrefs.GetInt("NetworkSettings_Port"));
    }
    public void ChangeVolumeAccordingToState()
    {
        switch(this.GameState.GetGameState())
        {
            case global::GameState.InMenu:
                {
                    AudioListener.volume = AudioSettings.GetUIVolume();
                    Debug.Log("Changing volumne to UI volume: " + AudioSettings.GetUIVolume());
                    break;
                }
            case global::GameState.InGame:
                {
                    AudioListener.volume = AudioSettings.GetAudioVolume();
                    Debug.Log("Changing volumne to game volume: " + AudioSettings.GetAudioVolume());
                    break;
                }
        }
    }
}
