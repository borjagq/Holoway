using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class WhiteboardTest : MonoBehaviour
{
    [Test]
    public void WhiteboardBound_Test()
    {
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

    [Test]
    public void Draw_Test()
    {
        var x = 10;
        var y = 10;
        var Whiteboard = new Whiteboard();
        Whiteboard.Draw(x, y);
        var expectedColor = Color.black;
        var color = Whiteboard.texture.GetPixel(x, y);
        var comparer = new ColorEqualityComparer(10e-5f);
        Assert.That(color, Is.EqualTo(expectedColor).Using(comparer));
    }
}
