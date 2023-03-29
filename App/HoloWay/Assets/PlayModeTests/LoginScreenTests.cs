using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[TestFixture]
public class LoginScreenTests : InputTestFixture
{
    private Mouse DefaultMouse;
    private Keyboard DefaultKeyboard;
    public override void Setup()
    {
        base.Setup();

        DefaultMouse = InputSystem.AddDevice<Mouse>();
        //DefaultMouse.Setup();
        DefaultKeyboard = InputSystem.AddDevice<Keyboard>();

        //DefaultKeyboard.Setup();
        SceneManager.LoadScene(0);
    }
    [UnityTest]
    public IEnumerator Test_CheckCamera()
    {
        yield return null;
        GameObject CameraObject = GameObject.Find("Main Camera");
        Assert.IsNotNull(CameraObject);
    }
    [UnityTest]
    public IEnumerator Test_CheckEventSystem()
    {
        yield return null;
        GameObject EventSystem = GameObject.Find("EventSystem");
        Assert.IsNotNull(EventSystem);
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvas()
    {
        yield return null;
        GameObject Canvas = GameObject.Find("UICanvas");
        Assert.IsNotNull(Canvas);
    }
    [UnityTest]
    public IEnumerator Test_CheckButton()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("UICanvas/LoginWindowBackground/LoginButton");
        Assert.IsNotNull(GUIElement);
    }
    [UnityTest]
    public IEnumerator Test_CheckUsernameField()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("UICanvas/LoginWindowBackground/UsernameField");
        Assert.IsNotNull(GUIElement);
    }
    [UnityTest]
    public IEnumerator Test_CheckPasswordField()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("UICanvas/LoginWindowBackground/PasswordField");
        Assert.IsNotNull(GUIElement);
    }

    [UnityTest]
    public IEnumerator Test_CheckUsernamePlaceholderText()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("UICanvas/LoginWindowBackground/UsernameField/Text Area/Placeholder");
        TMP_Text PlaceHolderText = GUIElement.GetComponent<TMP_Text>();
        Assert.AreEqual("Username", PlaceHolderText.text);
    }
    [UnityTest]
    public IEnumerator Test_CheckPasswordPlaceholderText()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("UICanvas/LoginWindowBackground/PasswordField/Text Area/Placeholder");
        TMP_Text PlaceHolderText = GUIElement.GetComponent<TMP_Text>();
        Assert.AreEqual("Password", PlaceHolderText.text);
    }
    [UnityTest]
    public IEnumerator Test_CheckLogin()
    {
        GameObject LoginButton = GameObject.Find("UICanvas/LoginWindowBackground/LoginButton");
        GameObject.Find("UsernameField").GetComponent<TMP_InputField>().text = "Sid";
        GameObject.Find("PasswordField").GetComponent<TMP_InputField>().text = "123456";
        
        LoginButton.GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(1.0f); //We need to wait for atleast some time in order to let the backend process our data
        LoginMenuScript Script = GameObject.Find("LoginMenuEventHandler").GetComponent<LoginMenuScript>();
        Assert.AreEqual(true, Script.HasStoredResponse());
        
    }
    /* [UnityTest]
     public IEnumerator Test_CheckCamera()
     {
         yield return null;
         GameObject InputField_Username = GameObject.Find("Main Camera");
         Assert.IsNotNull(InputField_Username);
     }
     //Test cases for password input box
     [UnityTest]
     public IEnumerator Test_CheckPasswordInput()
     {
         yield return null;
         GameObject InputField_Username = GameObject.Find("Canvas/InputField_Password");
         Assert.IsNotNull(InputField_Username);
     }
     [UnityTest]
     public IEnumerator Test_CheckPasswordInput_Textarea()
     {
         yield return null;
         GameObject InputField_Username_TextArea = GameObject.Find("Canvas/InputField_Password/Text Area");
         Assert.IsNotNull(InputField_Username_TextArea);
     }
     [UnityTest]
     public IEnumerator Test_CheckPasswordInput_TextArea_Placeholder()
     {
         yield return null;
         GameObject InputField_Username_TextArea_Placeholder = GameObject.Find("Canvas/InputField_Password/Text Area/Placeholder");
         Assert.IsNotNull(InputField_Username_TextArea_Placeholder);
     }
     [UnityTest]
     public IEnumerator Test_CheckPasswordInput_TextArea_PlaceholderText()
     {
         yield return null;
         GameObject InputField_Username_TextArea_Placeholder = GameObject.Find("Canvas/InputField_Password/Text Area/Placeholder");
         TMP_Text TextElement = InputField_Username_TextArea_Placeholder.GetComponent<TMP_Text>();
         Assert.AreEqual(TextElement.text, "Password");
     }
     [UnityTest]
     public IEnumerator Test_CheckPasswordInput_TextArea_Text()
     {
         yield return null;
         GameObject InputField_Username_TextArea_Text = GameObject.Find("Canvas/InputField_Password/Text Area/Text");
         Assert.IsNotNull(InputField_Username_TextArea_Text);
     }
     // Testcases for username input box
     [UnityTest]
     public IEnumerator Test_CheckUsernameInput()
     {
         yield return null;
         GameObject InputField_Username = GameObject.Find("Canvas/InputField_Username");
         Assert.IsNotNull(InputField_Username);
     }
     [UnityTest]
     public IEnumerator Test_CheckUsernameInput_Textarea()
     {
         yield return null;
         GameObject InputField_Username_TextArea = GameObject.Find("Canvas/InputField_Username/Text Area");
         Assert.IsNotNull(InputField_Username_TextArea);
     }
     [UnityTest]
     public IEnumerator Test_CheckUsernameInput_TextArea_Placeholder()
     {
         yield return null;
         GameObject InputField_Username_TextArea_Placeholder = GameObject.Find("Canvas/InputField_Username/Text Area/Placeholder");
         Assert.IsNotNull(InputField_Username_TextArea_Placeholder);
     }
     [UnityTest]
     public IEnumerator Test_CheckUsernameInput_TextArea_PlaceholderText()
     {
         yield return null;
         GameObject InputField_Username_TextArea_Placeholder = GameObject.Find("Canvas/InputField_Username/Text Area/Placeholder");
         TMP_Text TextElement = InputField_Username_TextArea_Placeholder.GetComponent<TMP_Text>();
         Assert.AreEqual(TextElement.text, "Username");
     }
     [UnityTest]
     public IEnumerator Test_CheckUsernameInput_TextArea_Text()
     {
         yield return null;
         GameObject InputField_Username_TextArea_Text = GameObject.Find("Canvas/InputField_Username/Text Area/Text");
         Assert.IsNotNull(InputField_Username_TextArea_Text);
     }
     //Checking the input system
     [UnityTest]
     public IEnumerator Test_UsernameField_Inputs()
     {
         yield return null;          //Skip a frame
         GameObject InputField_Username = GameObject.Find("Canvas/InputField_Username");
         GameObject InputField_Username_TextArea_Text = GameObject.Find("Canvas/InputField_Password/Text Area/Text");
         TMP_Text TextField = InputField_Username_TextArea_Text.GetComponent<TMP_Text>();
         Move(this.DefaultMouse.position, InputField_Username.transform.position);
         Click(this.DefaultMouse.leftButton);
         yield return null;          //Skip another frame
         Debug.Log(DefaultKeyboard.keyboardLayout);
         Press(DefaultKeyboard.aKey) ;
         yield return new WaitForSeconds(0.1f);          //Skip another frame
         Release(DefaultKeyboard.aKey);
         Press(DefaultKeyboard.bKey);
         yield return new WaitForSeconds(0.1f);          //Skip another frame
         Press(DefaultKeyboard.cKey);
         yield return new WaitForSeconds(0.1f);          //Skip another frame
         InputSystem.Update();
         yield return new WaitForSeconds(2);
         Assert.AreEqual(TextField.text, "abc");
     }
    */
}