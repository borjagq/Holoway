using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SettingsMenuTests
{
    [SetUp]
    public void Setup()
    {
        Debug.Log("Loading settings menu tests...");
        SceneManager.LoadScene(2);
    }
    // Check if the objects are loading properly...
    [UnityTest]
    public IEnumerator Test_CheckCamera()
    {
        yield return null; //Skip the frame
        Assert.NotNull(GameObject.Find("Main Camera"));
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvas()
    {
        yield return null; //Skip the frame
        Assert.NotNull(GameObject.Find("UICanvas"));
    }
    [UnityTest]
    public IEnumerator Test_CheckSettingsMenuPanel()
    {
        yield return null; //Skip the frame
        Assert.NotNull(GameObject.Find("UICanvas/SettingsMenu"));
    }
    [UnityTest]
    public IEnumerator Test_CheckSettingsMenuPanelHeaderBackground()
    {
        yield return null; //Skip the frame
        Assert.NotNull(GameObject.Find("UICanvas/SettingsMenu/SettingsMenuBackground/SettingsMenuHeaderBackground"));
    }
    [UnityTest]
    public IEnumerator Test_CheckSettingsMenuPanelHeaderText()
    {
        yield return null; //Skip the frame
        Assert.NotNull(GameObject.Find("UICanvas/SettingsMenu/SettingsMenuBackground/SettingsMenuHeader_Text"));
    }
    [UnityTest]
    public IEnumerator Test_CheckSettingsMenuPanelBackground()
    {
        yield return null; //Skip the frame
        Assert.NotNull(GameObject.Find("UICanvas/SettingsMenu/SettingsMenuBackground/Icon_Settings"));
    }
    [UnityTest]
    public IEnumerator Test_CheckSettingsMenuCheckButtons()
    {
        yield return null; //Skip the frame
        Assert.NotNull(GameObject.Find("UICanvas/SettingsMenu/Buttons"));
    }
    [UnityTest]
    public IEnumerator Test_CheckSettingsMenuPanelButtonsControlsButton()
    {
        yield return null; //Skip the frame
        Assert.NotNull(GameObject.Find("UICanvas/SettingsMenu/Buttons/Button_Controls"));
    }
    [UnityTest]
    public IEnumerator Test_CheckSettingsMenuPanelButtonsGraphicsButton()
    {
        yield return null; //Skip the frame
        Assert.NotNull(GameObject.Find("UICanvas/SettingsMenu/Buttons/Button_Graphics"));
    }
    [UnityTest]
    public IEnumerator Test_CheckSettingsMenuPanelButtonsDisplayButton()
    {
        yield return null; //Skip the frame
        Assert.NotNull(GameObject.Find("UICanvas/SettingsMenu/Buttons/Button_Display"));
    }
    [UnityTest]
    public IEnumerator Test_CheckSettingsMenuPanelButtonsAudioButton()
    {
        yield return null; //Skip the frame
        Assert.NotNull(GameObject.Find("UICanvas/SettingsMenu/Buttons/Button_Audio"));
    }
    [UnityTest]
    public IEnumerator Test_CheckSettingsMenuPanelButtonsBackButton()
    {
        yield return null; //Skip the frame
        Assert.NotNull(GameObject.Find("UICanvas/SettingsMenu/Buttons/Button_Back"));
    }

    //Check for functionality...

}
