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

    [UnityTest]
    public IEnumerator Test_ChangeSceneToSmallRoom()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/GameObject/InputField (TMP)");
        TMP_InputField ButtonInput = Object.GetComponent<TMP_InputField>();
        ButtonInput.text = "small";
        ButtonInput.onEndEdit.Invoke(ButtonInput.text);

        yield return new WaitForSeconds(3f);
        Assert.AreEqual(7, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest]
    public IEnumerator Test_ChangeSceneToMediumRoom()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/GameObject/InputField (TMP)");
        TMP_InputField ButtonInput = Object.GetComponent<TMP_InputField>();
        ButtonInput.text = "medium";
        ButtonInput.onEndEdit.Invoke(ButtonInput.text);

        yield return new WaitForSeconds(3f);
        Assert.AreEqual(8, SceneManager.GetActiveScene().buildIndex);
    }

    [UnityTest]
    public IEnumerator Test_ChangeSceneToLargeRoom()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/GameObject/InputField (TMP)");
        TMP_InputField ButtonInput = Object.GetComponent<TMP_InputField>();
        ButtonInput.text = "large";
        ButtonInput.onEndEdit.Invoke(ButtonInput.text);

        yield return new WaitForSeconds(3f);
        Assert.AreEqual(9, SceneManager.GetActiveScene().buildIndex);
    }
}
