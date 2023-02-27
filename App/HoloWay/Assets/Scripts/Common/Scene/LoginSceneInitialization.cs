using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginSceneInitialization : MonoBehaviour
{
    [Header("UI Core")]
    public GameObject DefaultCanvasPrefab;
    public GameObject DefaultEventSystemPrefab;

    [Header("UI Elements")]
    public GameObject DefaultInputFieldPrefab;
    public GameObject DefaultButtonPrefab;

    [Header("UI Event Elements")]
    public GameObject DefaultLoginMenuEventHandlerPrefab;

    [Header("Game Elements")]
    public GameObject DefaultCameraPrefab;


    // Private Vars


    // Start is called before the first frame 
    private void Awake()
    {
        GameObject DefaultCamera = GameObject.Instantiate(DefaultCameraPrefab);
        DefaultCamera.name = "Main Camera";

        GameObject DefaultLoginMenuEventHandler = GameObject.Instantiate(DefaultLoginMenuEventHandlerPrefab);
        DefaultLoginMenuEventHandler.name = "LoginMenuEventHandler";

        LoginMenuScript Script = DefaultLoginMenuEventHandler.GetComponent<LoginMenuScript>();

        GameObject DefaultCanvas = GameObject.Instantiate(DefaultCanvasPrefab);
        DefaultCanvas.name = "Canvas";
        DefaultCanvas.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        GameObject DefaultButton = GameObject.Instantiate(DefaultButtonPrefab);
        DefaultButton.name = "LoginButton";
        DefaultButton.transform.SetParent(DefaultCanvas.transform);
        DefaultButton.GetComponent<Button>().onClick.AddListener(Script.LoginButtonClick);
        DefaultButton.transform.position = new Vector3(0.0f, -50.0f, 0.0f);

        GameObject DefaultUsernameField = GameObject.Instantiate(DefaultInputFieldPrefab);
        DefaultUsernameField.name = "UsernameField";
        DefaultUsernameField.transform.SetParent(DefaultCanvas.transform);
        DefaultUsernameField.transform.position = new Vector3(0.0f, 50.0f, 0.0f);
        GameObject DefaultUsernameFieldPlaceHolder = GameObject.Find("Canvas/UsernameField/Text Area/Placeholder");
        DefaultUsernameFieldPlaceHolder.GetComponent<TMP_Text>().text = "Username";

        GameObject DefaultPasswordField = GameObject.Instantiate(DefaultInputFieldPrefab);
        DefaultPasswordField.name = "PasswordField";
        DefaultPasswordField.GetComponent<TMP_InputField>().contentType = TMP_InputField.ContentType.Password;
        /*DefaultPasswordField.GetComponent<InputField>().contentType = InputField.ContentType.Password;*/
        DefaultPasswordField.transform.SetParent(DefaultCanvas.transform);
        DefaultPasswordField.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        GameObject DefaultPasswordFieldPlaceHolder = GameObject.Find("Canvas/PasswordField/Text Area/Placeholder");
        DefaultPasswordFieldPlaceHolder.GetComponent<TMP_Text>().text = "Password";



        GameObject DefaultEventSystem = GameObject.Instantiate(DefaultEventSystemPrefab);
        DefaultEventSystem.name = "EventSystem";

    }
}
