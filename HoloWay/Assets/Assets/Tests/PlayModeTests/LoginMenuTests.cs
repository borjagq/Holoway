using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class LoginMenuTests : InputTestFixture
{
    [SetUp]
    public override void Setup()
    {
        SceneManager.LoadScene(1);
    }

    [UnityTest]
    public IEnumerator Test_UICanvas()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas");
        Assert.NotNull(Object);
    }


    [UnityTest]
    public IEnumerator Test_Background()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/Background");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_BackgroundOverlay()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/BackgroundOverlay");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_Logo()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/Logo");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_LoginCodeText()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/LoginCodeText");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_LoginCode()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/LoginCode");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_VisitWebsite()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/VisitWebsite");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_LoginManager()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/LoginManager");
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
    public IEnumerator Test_LoginMenuEventHandler()
    {
        yield return null;
        GameObject Object = GameObject.Find("LoginMenuEventHandler");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_MainCamera()
    {
        yield return null;
        GameObject Object = GameObject.Find("Main Camera");
        Assert.NotNull(Object);
    }
}
