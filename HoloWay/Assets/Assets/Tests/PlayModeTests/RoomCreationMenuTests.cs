using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RoomCreationMenuTests
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(5);
    }

    [UnityTest]
    public IEnumerator Test_UICanvas()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas");
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
    public IEnumerator Test_DirectionalLight()
    {
        yield return null;
        GameObject Object = GameObject.Find("Directional Light");
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
    public IEnumerator Test_ChangeSceneRoomSelection()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/GameObject/ButtonMenu");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(6, SceneManager.GetActiveScene().buildIndex);
    }
}
