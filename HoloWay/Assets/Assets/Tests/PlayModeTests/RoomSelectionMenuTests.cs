using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RoomSelectionMenuTests
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(6);
    }

    [UnityTest]
    public IEnumerator Test_UICanvas()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_RoomBrowse()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/RoomBrowse");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_MenuBackground()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/RoomBrowse/MenuBackground");
        Assert.NotNull(Object);
    }



    [UnityTest]
    public IEnumerator Test_Buttons()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/RoomBrowse/Buttons");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonRoom01()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/RoomBrowse/Buttons/Button_Room01");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonRoom02()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/RoomBrowse/Buttons/Button_Room02");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonRoom03()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/RoomBrowse/Buttons/Button_Room03");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_ButtonBack()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/RoomBrowse/Buttons/Button_Back");
        Assert.NotNull(Object);
    }


    [UnityTest]
    public IEnumerator Test_TextBg()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/TextBg");
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
    public IEnumerator Test_ChangeSceneSmallRoom()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/RoomBrowse/Buttons/Button_Room01");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(7, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest]
    public IEnumerator Test_ChangeSceneMediumRoom()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/RoomBrowse/Buttons/Button_Room02");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();
        yield return new WaitForSeconds(2f);
        Assert.AreEqual(8, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest]
    public IEnumerator Test_ChangeSceneLargeRoom()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/RoomBrowse/Buttons/Button_Room03");
        Button button = Object.GetComponent<Button>();
        button.onClick.Invoke();
        yield return new WaitForSeconds(2f);

        Debug.Log("LARGE ROOM TESTS");
        Debug.Log(SceneManager.GetActiveScene().buildIndex);

        Assert.AreEqual(9, SceneManager.GetActiveScene().buildIndex);
    }
}
