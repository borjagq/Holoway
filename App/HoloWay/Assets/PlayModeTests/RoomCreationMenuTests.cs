using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class RoomCreationMenuTests 
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(1);
    }
    [UnityTest]
    public IEnumerator Test_CheckCanvas()
    {
        yield return null;          // skip a frame
        GameObject UICanvas = GameObject.Find("UICanvas");
        Assert.IsNotNull(UICanvas);
    }
}
