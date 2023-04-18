using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using TMPro;

public class SplashScreenTests : InputTestFixture
{
    [SetUp]
    public override void Setup()
    {
        SceneManager.LoadScene(0);
    }

    [UnityTest]
    public IEnumerator Test_UICanvas()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas");
        Assert.NotNull(Object);
    }


    [UnityTest]
    public IEnumerator Test_RawImage()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/RawImage");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_CoverImage()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/CoverImage");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_EventSystem()
    {
        yield return null;
        GameObject Object = GameObject.Find("EventSystem");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_MainCamera()
    {
        yield return null;
        GameObject Object = GameObject.Find("MainCamera");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_VideoPlayer()
    {
        yield return null;
        GameObject Object = GameObject.Find("VideoPlayer");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ScriptGameObject()
    {
        yield return null;
        GameObject Object = GameObject.Find("ScriptGameObject");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ChangeSceneLoginScreen()
    {
        yield return new WaitForSeconds(10f);

        Assert.AreEqual(1, SceneManager.GetActiveScene().buildIndex);
    }
}