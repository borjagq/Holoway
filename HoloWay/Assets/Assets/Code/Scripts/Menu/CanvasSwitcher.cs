using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CanvasSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(XRSettings.enabled)
        {
            this.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        }
        else
        {
            //Reset to the overlay settings..
            this.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }

}
