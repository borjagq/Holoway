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
        SceneManager.LoadScene(2);
    }

    [UnityTest]
    public IEnumerator Test_UICanvas()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_MainMenu()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_MainMenuBackground()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_backgroundLayer()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/backgroundLayer");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_Body()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_Logo()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body/Logo");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_Buttons()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body/Buttons");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonAccessRooms()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body/Buttons/ButtonMenu Access Rooms");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonChangeAvatar()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body/Buttons/ButtonMenu Change your Avatar");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonSettings()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body/Buttons/ButtonMenu Settings");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonSignOut()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body/Buttons/ButtonMenu Sign out");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonReturnToDesktop()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body/Buttons/ButtonMenu Return to Desktop");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_MainCamera()
    {
        yield return null;
        GameObject Object = GameObject.Find("Main Camera");
        Assert.NotNull(Object);
    }


    [UnityTest]
    public IEnumerator Test_ChangeSceneAccessRooms()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body/Buttons/ButtonMenu Access Rooms");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(5, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest]
    public IEnumerator Test_ChangeSceneAvatarRoom()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body/Buttons/ButtonMenu Change your Avatar");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(4, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest]
    public IEnumerator Test_ChangeSceneSignout()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body/Buttons/ButtonMenu Sign out");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(0, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest]
    public IEnumerator Test_ChangeSceneSettings()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body/Buttons/ButtonMenu Settings");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(3, SceneManager.GetActiveScene().buildIndex);
    }

    //[UnityTest]
    //public IEnumerator Test_CheckCanvas()
    //{
    //    yield return null; //Skip a frame
    //    Assert.NotNull(GameObject.Find("UICanvas"));
    //}

    //[UnityTest]
    //public IEnumerator Test_CheckPlayDemoButton()
    //{
    //    yield return null; //Skip a frame
    //    Assert.NotNull(GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Buttons/Button_DemoPlay"));
    //}
    //[UnityTest]
    //public IEnumerator Test_CheckPlayDemoOnClick()
    //{
    //    yield return null; //Skip a frame
    //    GameObject Canvas = GameObject.Find("UICanvas");
    //    MainMenuScript script = Canvas.GetComponent<MainMenuScript>();
    //    script.PlayDemo_OnClick();
    //    yield return new WaitForSeconds(3.5f);
    //    Assert.AreEqual(5, SceneManager.GetActiveScene().buildIndex);
    //}

    //[UnityTest]
    //public IEnumerator Test_CheckAvatarCustomizationButton()
    //{
    //    yield return null; //Skip a frame
    //    Assert.NotNull(GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Buttons/Button_Avatar"));
    //}
    //[UnityTest]
    //public IEnumerator Test_CheckAvatarCustomizationOnClick()
    //{
    //    yield return null; //Skip a frame
    //    GameObject Canvas = GameObject.Find("UICanvas");
    //    MainMenuScript script = Canvas.GetComponent<MainMenuScript>();
    //    script.Avatar_OnClick();
    //    yield return new WaitForSeconds(3.5f);
    //    Assert.AreEqual(6, SceneManager.GetActiveScene().buildIndex);
    //}

    //[UnityTest]
    //public IEnumerator Test_CheckRoomsButton()
    //{
    //    yield return null; //Skip a frame
    //    Assert.NotNull(GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Buttons/Button_BrowseRooms"));
    //}
    //[UnityTest]
    //public IEnumerator Test_CheckRoomsButtonOnClick()
    //{
    //    yield return null; //Skip a frame
    //    GameObject Canvas = GameObject.Find("UICanvas");
    //    MainMenuScript script = Canvas.GetComponent<MainMenuScript>();
    //    script.BrowseRooms_OnClick();
    //    yield return new WaitForSeconds(1.5f);
    //    Assert.AreEqual(2, SceneManager.GetActiveScene().buildIndex);
    //}

    //[UnityTest]
    //public IEnumerator Test_CheckSettingsButton()
    //{
    //    yield return null; //Skip a frame
    //    Assert.NotNull(GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Buttons/Button_Settings"));
    //}
    //[UnityTest]
    //public IEnumerator Test_CheckSettingsButtonOnClick()
    //{
    //    yield return null; //Skip a frame
    //    GameObject Canvas = GameObject.Find("UICanvas");
    //    MainMenuScript script = Canvas.GetComponent<MainMenuScript>();
    //    script.Settings_OnClick();
    //    yield return new WaitForSeconds(1.5f);
    //    Assert.AreEqual(4, SceneManager.GetActiveScene().buildIndex);
    //}

    //[UnityTest]
    //public IEnumerator Test_CheckLogoutButton()
    //{
    //    yield return null; //Skip a frame
    //    Assert.NotNull(GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Buttons/Button_Logout"));
    //}
    //[UnityTest]
    //public IEnumerator Test_CheckLogoutButtonOnClick()
    //{
    //    yield return null; //Skip a frame
    //    GameObject Canvas = GameObject.Find("UICanvas");
    //    MainMenuScript script = Canvas.GetComponent<MainMenuScript>();
    //    script.Logout_OnClick();
    //    yield return new WaitForSeconds(1.5f);
    //    Assert.AreEqual(0, SceneManager.GetActiveScene().buildIndex);
    //}

    //[UnityTest]
    //public IEnumerator Test_QuitButton()
    //{
    //    yield return null; //Skip a frame
    //    Assert.NotNull(GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Buttons/Button_Quit"));
    //}
    //[UnityTest]
    //public IEnumerator Test_CheckQuitButtonOnClick()
    //{
    //    yield return null; //Skip a frame
    //    GameObject Canvas = GameObject.Find("UICanvas");
    //    MainMenuScript script = Canvas.GetComponent<MainMenuScript>();
    //    script.Quit_OnClick();
    //    yield return new WaitForSeconds(1.5f);
    //    Assert.AreEqual(2, SceneManager.GetActiveScene().buildIndex);
    //}
}