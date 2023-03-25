using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuTests
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(1);
    }

    [UnityTest]
    public IEnumerator Test_CheckCanvas()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas"));
    }

    [UnityTest]
    public IEnumerator Test_CheckPlayDemoButton()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Buttons/Button_DemoPlay"));
    }
    [UnityTest]
    public IEnumerator Test_CheckPlayDemoOnClick()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        MainMenuScript script = Canvas.GetComponent<MainMenuScript>();
        script.PlayDemo_OnClick();
        yield return new WaitForSeconds(3.5f);
        Assert.AreEqual(5, SceneManager.GetActiveScene().buildIndex);
    }
    
    [UnityTest]
    public IEnumerator Test_CheckAvatarCustomizationButton()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Buttons/Button_Avatar"));
    }
    [UnityTest]
    public IEnumerator Test_CheckAvatarCustomizationOnClick()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        MainMenuScript script = Canvas.GetComponent<MainMenuScript>();
        script.Avatar_OnClick();
        yield return new WaitForSeconds(3.5f);
        Assert.AreEqual(6, SceneManager.GetActiveScene().buildIndex);
    }
    
    [UnityTest]
    public IEnumerator Test_CheckRoomsButton()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Buttons/Button_BrowseRooms"));
    }
    [UnityTest]
    public IEnumerator Test_CheckRoomsButtonOnClick()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        MainMenuScript script = Canvas.GetComponent<MainMenuScript>();
        script.BrowseRooms_OnClick();
        yield return new WaitForSeconds(1.5f);
        Assert.AreEqual(2, SceneManager.GetActiveScene().buildIndex);
    }
    
    [UnityTest]
    public IEnumerator Test_CheckSettingsButton()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Buttons/Button_Settings"));
    }
    [UnityTest]
    public IEnumerator Test_CheckSettingsButtonOnClick()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        MainMenuScript script = Canvas.GetComponent<MainMenuScript>();
        script.Settings_OnClick();
        yield return new WaitForSeconds(1.5f);
        Assert.AreEqual(4, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest]
    public IEnumerator Test_CheckLogoutButton()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Buttons/Button_Logout"));
    }
    [UnityTest]
    public IEnumerator Test_CheckLogoutButtonOnClick()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        MainMenuScript script = Canvas.GetComponent<MainMenuScript>();
        script.Logout_OnClick();
        yield return new WaitForSeconds(1.5f);
        Assert.AreEqual(0, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest]
    public IEnumerator Test_QuitButton()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Buttons/Button_Quit"));
    }
    [UnityTest]
    public IEnumerator Test_CheckQuitButtonOnClick()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        MainMenuScript script = Canvas.GetComponent<MainMenuScript>();
        script.Quit_OnClick();
        yield return new WaitForSeconds(1.5f);
        Assert.AreEqual(2, SceneManager.GetActiveScene().buildIndex);
    }
}
