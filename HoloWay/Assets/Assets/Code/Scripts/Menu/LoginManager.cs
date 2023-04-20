using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Collections;
using TMPro;
using holowayapi;

public class LoginManager : MonoBehaviour
{

    private string api_key;
    private string priv_key;

    private string api_code;

    /*
     * Start is called before the first frame update.
     */
    void Start()
    {
        //GlobalGameSettings.CreateInstance();
        this.api_key = "N7e9vMq3BMmD84XxwUI4Vhq1snt7iBe8";
        this.priv_key = Application.dataPath + "/Assets/Code/Scripts/HolowayAPI/N7e9vMq3BMmD84XxwUI4Vhq1snt7iBe8.xml";

        GameObject LoginCodeText = GameObject.Find("LoginCode");
        HolowayAPI api = LoginCodeText.AddComponent<HolowayAPI>();

        api.add_params(priv_key, api_key);

        // Get the stored session token.
        if (PlayerPrefs.HasKey("player_token")) {

            string player_token = PlayerPrefs.GetString("player_token");

            if (player_token != "") {

                // Call to check if it works.
                StartCoroutine(api.check_credential(player_token, CheckedLogin));

                return;

            }

        }
        
        GetLoginCode();

    }

    /*
     * Get a lodin code into the UI.
     */
    void GetLoginCode()
    {

        GameObject LoginCodeText = GameObject.Find("LoginCode");

        HolowayAPI api = LoginCodeText.GetComponent<HolowayAPI>();

        StartCoroutine(api.create_login(ReplaceCode));

    }

    /*
     * This is the function that gets called when create_login finishes.
     * It replaces the code in the UI and proceeds to check if it has been used.
     */
    void ReplaceCode(string status, string code, string msg)
    {

        if (status != "success")
            return;

        GameObject LoginCodeText = GameObject.Find("LoginCode");

        if (LoginCodeText == null) {
            return;
        }
        
        TMP_Text LoginText = LoginCodeText.GetComponent<TMP_Text>();
        
        if (LoginText == null) {
            return;
        }
        
        LoginText.text = code;

        this.api_code = code;

        InvokeRepeating("RepeatCheck", 2.0f, 2.0f);

    }

    /*
     * Repeatedly called to check for the login code.
     */
    void RepeatCheck()
    {

        GameObject LoginCodeText = GameObject.Find("LoginCode");

        HolowayAPI api = LoginCodeText.GetComponent<HolowayAPI>();

        StartCoroutine(api.retrieve_login(api_code, CheckIfLoggedIn));

    }

    /*
     * This function gets called when retrieve_login is executed.
     * Checks if the login has been retrieved or not, and if it is, stores the
     * login data and changes the page.
     */
    void CheckIfLoggedIn(string status, string user_id, string name, string token, string msg)
    {

        if (status == "success" && token != "") {

            PlayerPrefs.SetString("player_token", token);

            LoginSession.token = token;
            LoginSession.user_id = user_id;
            LoginSession.user_name = name;

            CancelInvoke("RepeatCheck");
            if (name != null)
            {
                if (name != "")
                {
                    if (name.Length > 0)
                        GlobalGameSettings.Instance.SessionSettings.SetSessionName(name);
                }
            }
            SceneManager.LoadScene(2);

        }

        Debug.Log("Status: " + status);
        Debug.Log("email: " + user_id);
        Debug.Log("name: " + name);
        Debug.Log("token: " + token);
        Debug.Log("msg: " + msg);

    }

    /*
     * This function gets called when check_credentials is executed.
     * Checks if the login has been retrieved or not, and if it is, stores the
     * login data and changes the page. Otherwise, it shows a code login.
     */
    void CheckedLogin(string status, string user_id, string name, string token, string msg)
    {

        if (status == "success") {

            PlayerPrefs.SetString("player_token", token);

            LoginSession.token = token;
            LoginSession.user_id = user_id;
            LoginSession.user_name = name;

            if (name != null)
            {
                if (name != "")
                {
                    if (name.Length > 0)
                        GlobalGameSettings.Instance.SessionSettings.SetSessionName(name);
                }
            }

            SceneManager.LoadScene(2);

        } else {

            PlayerPrefs.DeleteKey("player_token");

            GetLoginCode();

        }

        Debug.Log("Status: " + status);
        Debug.Log("email: " + user_id);
        Debug.Log("name: " + name);
        Debug.Log("token: " + token);
        Debug.Log("msg: " + msg);

    }

}
