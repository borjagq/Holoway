using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginMenuScript : MonoBehaviour
{
    public Queue<string> QueryResponses = new Queue<string>();
    private string URL_LOGIN = "http://localhost:3000/DefaultApi/Login";
    ///private string URL_LOGIN_SECURE = "https://localhost:3000/DefaultApi/Login";
    public void LoginButtonClick()
    {
        StartCoroutine(DoLogin());
        //SceneManager.LoadScene(GlobalGameSettings.SCENE_INDEX_MAINMENU);
    }
    public IEnumerator DoLogin()
    {
        string Username = GameObject.Find("UsernameField").GetComponent<TMP_InputField>().text;
        string Password = GameObject.Find("PasswordField").GetComponent<TMP_InputField>().text;
        string URL_QUERY = URL_LOGIN + "?Username=" + Username + "&Password=" + Password;
        Debug.Log(URL_QUERY);

        using(UnityWebRequest request = UnityWebRequest.Get(URL_QUERY))
        {
            yield return request.SendWebRequest();
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(request.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(request.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(":\nReceived: " + request.downloadHandler.text);
                    QueryResponses.Enqueue(request.downloadHandler.text);
                    break;
            }
        }
    }
    public bool HasStoredResponse()
    {
        return QueryResponses.Count > 0;
    }

}
