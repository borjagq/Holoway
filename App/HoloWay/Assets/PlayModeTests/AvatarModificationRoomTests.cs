using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class AvatarModificationRoomTests : InputTestFixture
{
    [SetUp]
    public override void Setup()
    {
        SceneManager.LoadScene(6);
    }
    [UnityTest]
    public IEnumerator Test_CheckMainCamera()
    {
        yield return null;
        GameObject Object = GameObject.Find("Main Camera");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CheckLight()
    {
        yield return null;
        GameObject Object = GameObject.Find("Directional Light");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CheckChangingRoomBase()
    {
        yield return null;
        GameObject Object = GameObject.Find("Changing Room");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CheckUMAGLIB()
    {
        yield return null;
        GameObject Object = GameObject.Find("UMA_GLIB");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CheckDefaultPlayer()
    {
        yield return null;
        GameObject Object = GameObject.Find("Player");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CheckUICanvas()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterModificationPanel()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/CharacterModificationPanel");
        Assert.NotNull(Object);
    }

}
