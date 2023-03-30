using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioDectection : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip microphoneClip;

    // Start is called before the first frame update
    void Start()
    {
        MicrophoneToAudioClipAsync();
    }

    public async Task MicrophoneToAudioClipAsync()
    {
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);

        SavWav.Save("J:/Unity_Project/ASE_Project/ase-team3-meeting_room/App/HoloWayAssets/audio/myaudio", microphoneClip);

        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add(
              "Authorization",
              "bdf62e371331482f8e5af4004590de54"
            );

            var json = new
            {
                audio_url = "J:/Unity_Project/ASE_Project/ase-team3-meeting_room/App/HoloWayAssets/audio/myaudio.wav"
            };

            StringContent payload = new StringContent(
              JsonConvert.SerializeObject(json),
              Encoding.UTF8,
              "application/json"
            );

            HttpResponseMessage response = await httpClient.PostAsync(
              "https://api.assemblyai.com/v2/transcript",
              payload
            );

            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            Debug.Log("hello" +responseJson.ToString());

        }
    }
    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetLoudnessFromAudioClip(int clipPosition,AudioClip clip)
    {
        int starPosition = clipPosition - sampleWindow;

        if (starPosition < 0)
            return 0;

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, starPosition);

        //compute loudness
        float totalLoudness = 0;

        for(int i =0;i<sampleWindow;i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        return totalLoudness / sampleWindow;
    }
}
