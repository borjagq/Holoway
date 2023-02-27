using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class RoomCreationMenuTests
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(2);
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvas()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas"));
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasBackground()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/Background"));
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasCreateRoom()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/Background/CreateRoomPanel"));
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasJoinRoom()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/Background/JoinRoomPanel"));
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasCreateRoomCreateEmptyRoomText()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/Background/CreateRoomPanel/Text_CreateEmptyRoom"));
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasJoinRoomJoinExistingRoomText()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/Background/JoinRoomPanel/Text_CreateEmptyRoom"));
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasCreateRoomCreateEmptyRoomButton()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/Background/CreateRoomPanel/Button_EnterRoom"));
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasButtonBack()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/Background/Button_Back"));
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasButtonConfirm()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("UICanvas/Background/Button_Confirm"));
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasCheckScript()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        RoomCreationMenuScript script = Canvas.GetComponent<RoomCreationMenuScript>();
        Assert.NotNull(script);
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasBackButtonOnClick()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        RoomCreationMenuScript script = Canvas.GetComponent<RoomCreationMenuScript>();
        script.BackButtonOnClick();
        yield return new WaitForSeconds(1.5f);
        Assert.AreEqual(1, SceneManager.GetActiveScene().buildIndex);

    }
    [UnityTest]
    public IEnumerator Test_CheckCanvasCreateRoomOnClick()
    {
        yield return null; //Skip a frame
        GameObject Canvas = GameObject.Find("UICanvas");
        RoomCreationMenuScript script = Canvas.GetComponent<RoomCreationMenuScript>();
        script.CreateRoomButtonOnClick();
        yield return new WaitForSeconds(1.5f);
        Assert.AreEqual(3, SceneManager.GetActiveScene().buildIndex);

    }
}
