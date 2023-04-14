using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using holowayapi;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    private const string api_key = "N7e9vMq3BMmD84XxwUI4Vhq1snt7iBe8";
    private const string priv_key = "Assets/Code/Scripts/HolowayAPI/N7e9vMq3BMmD84XxwUI4Vhq1snt7iBe8.xml";
    // Start is called before the first frame update
    void Start()
    {
        GameObject LoginCodeText = GameObject.Find("LoginCode");
        holowayapi.HolowayAPI api = LoginCodeText.AddComponent<holowayapi.HolowayAPI>();
        api.add_params(priv_key, api_key);
        (string status, string code, string msg) = api.create_login();
        
        Debug.Log("status : " + status);
        Debug.Log("msg : " + msg);
        if (LoginCodeText == null) {
            return;
        }
            
        
        TMP_Text LoginText = LoginCodeText.GetComponent<TMP_Text>();
        
        if (LoginText == null){
            return;
        }
            
        
        LoginText.text = code;
    }


    public void MoveToScene() {
        SceneManager.LoadScene(1);
    }
}
