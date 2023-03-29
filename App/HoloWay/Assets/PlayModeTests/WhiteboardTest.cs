using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class WhiteboardTest : MonoBehaviour
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(7);
    }

    [UnityTest]
    public IEnumerator CheckWhiteboard_Test()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("Whiteboard"));
    }

    [UnityTest]
    public IEnumerator CheckMarkers_Test()
    {
        yield return null; //Skip a frame
        Assert.NotNull(GameObject.Find("Marker"));
    }

    [UnityTest]
    public IEnumerator WhiteboardBound_Test()
    {
        yield return null; //Skip a frame
        var x = 10;
        var y = 10;
        var Whiteboard = new Whiteboard();
        var expectedResult = 0;
        if (x < 0 || x > Whiteboard.textureSize.x || y < 0 || y > Whiteboard.textureSize.y)
        {
            expectedResult = -1;
        }
        else
        {
            expectedResult = 1;
        }
        var result = Whiteboard.InsideWhiteboard(x, y);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [UnityTest]
    public IEnumerator Draw_Test()
    {
        yield return null; //Skip a frame
        var x = 10;
        var y = 10;
        var Whiteboard = new Whiteboard();
        Whiteboard.Draw(x, y);
        var expectedColor = Color.black;
        var color = Whiteboard.texture.GetPixel(x, y);
        Assert.That(color, Is.EqualTo(expectedColor));
    }
}
