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
        SceneManager.LoadScene(5);
    }

    [UnityTest]
    public IEnumerator Test_CheckCanvas()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas"));
    }

    [UnityTest]
    public IEnumerator Test_Room1ButtonExistance()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/RoomBrowse/Buttons/Button_Room01"));
    }
    [UnityTest]
    public IEnumerator Test_Room2ButtonExistance()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/RoomBrowse/Buttons/Button_Room02"));
    }
    [UnityTest]
    public IEnumerator Test_Room3ButtonExistance()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/RoomBrowse/Buttons/Button_Room03"));
    }

    // Unity Test to check if the id of the selected room is the correct one
    [UnityTest]
    public IEnumerator Test_RoomSelectionButton1()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        RoomSelectionMenuScript script = Canvas.GetComponent<RoomSelectionMenuScript>();
        script.SelectRoomOneOnClick();
        yield return new WaitForSeconds(1.5f);
        Assert.AreEqual(1, script.GetSelectedRoomId());
    }
    [UnityTest]
    public IEnumerator Test_RoomSelectionButton2()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        RoomSelectionMenuScript script = Canvas.GetComponent<RoomSelectionMenuScript>();
        script.SelectRoomTwoOnClick();
        yield return new WaitForSeconds(1.5f);
        Assert.AreEqual(2, script.GetSelectedRoomId());
    }
    [UnityTest]
    public IEnumerator Test_RoomSelectionButton3()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        RoomSelectionMenuScript script = Canvas.GetComponent<RoomSelectionMenuScript>();
        script.SelectRoomThreeOnClick();
        yield return new WaitForSeconds(1.5f);
        Assert.AreEqual(3, script.GetSelectedRoomId());
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasButtonBack()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/RoomBrowse/Buttons/Button_Back"));
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasBackButtonOnClick()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        RoomSelectionMenuScript script = Canvas.GetComponent<RoomSelectionMenuScript>();
        script.BackButtonOnClick();
        yield return new WaitForSeconds(1.5f);
        Assert.AreEqual(2, SceneManager.GetActiveScene().buildIndex);

    }
}
