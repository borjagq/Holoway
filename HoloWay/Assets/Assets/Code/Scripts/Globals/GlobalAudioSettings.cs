using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class GlobalAudioSettings {

    private float AudioVolume = 1.0f;
    public float GetAudioVolume()
    {
        return this.AudioVolume;
    }
    public void SetAudioVolume(float volume)
    {
        AudioListener.volume = volume;
        this.AudioVolume = volume;
    }
    
}
