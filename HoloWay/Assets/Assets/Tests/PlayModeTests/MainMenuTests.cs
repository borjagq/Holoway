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

    [UnityTest]
    public IEnumerator Test_ChangeSceneReturnToDesktop()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/MainMenu/MainMenuBackground/Body/Buttons/ButtonMenu Return to Desktop");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();
        yield return new WaitForSeconds(4f);

        Assert.IsTrue(UnityEditor.EditorApplication.isPlaying);      
    }

}