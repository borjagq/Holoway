using System.Net;
using System.IO;
using UnityEngine;

public static class APIHelper 
{
    public static ExternalAPIData getNewExternalAPIData()
    {
        // initiate the API call by defining web request and response objects
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.chucknorris.io/jokes/random");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        // read the content of the response into a stream reader
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        return JsonUtility.FromJson<ExternalAPIData>(jsonResponse);
    }

}
