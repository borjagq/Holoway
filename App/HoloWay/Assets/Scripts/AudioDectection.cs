using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class AudioDectection : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip microphoneClip;
    private string id;

    // Start is called before the first frame update
    void Start()
    {
        MicrophoneToAudioClipAsync();
    }

    private static async Task<string> SendFile(HttpClient client, string filePath)
    {
        try
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "upload");
           
            request.Headers.Add("Transer-Encoding", "chunked");
            // Path to the audio file
            var path = @"D:\Facultate\Advance Software\HoloWay-Final\ase-team3-meeting_room\App\HoloWay\Assets\audioscript.mp4";
            var fileReader = File.OpenRead(path);
            Debug.Log("file read");
            var streamContent = new StreamContent(fileReader);
            request.Content = streamContent;
            HttpResponseMessage response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }
    public async Task MicrophoneToAudioClipAsync()
    {
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
        Debug.Log("I am here I will save the file");
        var filename = "myaudio";
        var filepath = Path.Combine(Application.dataPath, filename);
        // this is the problem
        SavWav.Save(filepath, microphoneClip);
        string API_Key = "bdf62e371331482f8e5af4004590de54";
        
        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri("https://api.assemblyai.com/v2/");
            httpClient.DefaultRequestHeaders.Add("authorization", API_Key);
            Debug.Log("We save the file");
            var jsonResult = await SendFile(httpClient, filepath);
            httpClient.Dispose();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.assemblyai.com/v2/");
            client.DefaultRequestHeaders.Add("authorization", API_Key);

            var json = new { audio_url = JsonConvert.DeserializeObject<UploadItem>(jsonResult).upload_url };
            var payload = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");

            Debug.Log("Make another call");
            var response = await client.PostAsync("https://api.assemblyai.com/v2/transcript", payload);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            Debug.Log(responseJson);
            var responseJson2 = (JObject)JsonConvert.DeserializeObject(responseJson);
            string id = responseJson2["id"].ToString();
            if (id != null)
            {
                Debug.Log("We have the id");
                HttpClient client2 = new HttpClient();
                client2.BaseAddress = new Uri("https://api.assemblyai.com/v2/");
                client2.DefaultRequestHeaders.Add("Authorization", API_Key);
                client2.DefaultRequestHeaders.Add("Accepts", "application/json");
                //"r55nxjfalm-1b41-42c2-acae-ec79c7a53141"
                var ticketId = "r5975vtnga-95af-4f76-a0f5-5f8daa613c59";
                var response2 = await client2.GetAsync("https://api.assemblyai.com/v2/transcript/" + ticketId);
                response2.EnsureSuccessStatusCode();
                Debug.Log("We tried to retrieve the text");
                var responseJson3 = await response2.Content.ReadAsStringAsync();
                Debug.Log(responseJson3);
                var responseJson4 = (JObject)JsonConvert.DeserializeObject(responseJson3);
                string text = responseJson4["text"].ToString();
                Debug.Log("We arrive here");
                Debug.Log(text);
            }
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

    class UploadItem
    {
        public string upload_url { get; set; }
    }
}
