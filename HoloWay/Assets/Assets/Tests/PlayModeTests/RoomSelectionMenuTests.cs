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
        
}
