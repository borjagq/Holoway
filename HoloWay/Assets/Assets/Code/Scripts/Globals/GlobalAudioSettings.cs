using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class GlobalAudioSettings {

    private float AudioVolume = 1.0f;
    private float MicrophoneVolume = 1.0f;
    private float UIVolume = 1.0f;
    public float GetAudioVolume()
    {
        return this.AudioVolume;
    }
    public void SetAudioVolume(float volume)
    {
        AudioListener.volume = volume;
        this.AudioVolume = volume;
    }

    public void SetMicrophoneVolume(float value)
    {
        this.MicrophoneVolume = value;
    }
    public void SetUIVolume(float value)
    {
        this.UIVolume = value;
    }
    public float GetMicrophoneVolume()
    {
        return this.MicrophoneVolume;
    }
    public float GetUIVolume()
    {
        return this.UIVolume;
    }
}
