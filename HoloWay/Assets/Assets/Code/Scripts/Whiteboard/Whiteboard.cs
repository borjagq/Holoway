using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);

    void Start()
    {
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
    }

    public int InsideWhiteboard(int x, int y)
    {
        if (x < 0 || x > textureSize.x || y < 0 || y > textureSize.y)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    public void Draw(int x, int y)
    {
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        Color black = Color.black;
        if (InsideWhiteboard(x, y) == 1)
        {
            texture.SetPixel(x, y, black);
        }
        texture.Apply();
    }
}
