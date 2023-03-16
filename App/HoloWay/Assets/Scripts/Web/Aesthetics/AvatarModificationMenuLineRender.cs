using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarModificationMenuLineRender : MonoBehaviour
{
    public LineRenderer Renderer;
    public Material LineMaterial;
    // Start is called before the first frame update
    public GameObject StartPosition;
    public GameObject EndPosition;
    public GameObject TargetTransform;
    public bool DrawBackToLine = false;
    public float lineWidth = 0.005f;
    
    void Start()
    {
        if(this.GetComponent<LineRenderer>() == null)
        {
            Renderer = this.gameObject.AddComponent<LineRenderer>();
        }
        Transform[] children = this.gameObject.GetComponentsInChildren<Transform>();
        foreach(Transform child in children)
        {
            Debug.Log(child.name);
            if (child.name == "Button_LineRenderer_Start") StartPosition = child.gameObject;
            if (child.name == "Button_LineRenderer_End") EndPosition = child.gameObject;
        }
        Renderer.material = LineMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer.startWidth = lineWidth;
        Renderer.endWidth = lineWidth;
        StartPosition.transform.position = TargetTransform.transform.position;
        Renderer.SetPosition(0, StartPosition.transform.position);
        Renderer.SetPosition(1, EndPosition.transform.position);
    }
}
