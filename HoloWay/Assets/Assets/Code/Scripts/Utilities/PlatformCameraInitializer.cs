using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlatformCameraInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject WindowsPlatformCamera;
    public GameObject XRPlatformCamera;
    void Start()
    {
        WindowsPlatformCamera.SetActive(false);
        XRPlatformCamera.SetActive(false);
        if (XRSettings.enabled)
        {
            Debug.Log("XR platform detected!");
            XRPlatformCamera.SetActive(true);
        }
        else
        {
            Debug.Log("Windows platform detected!");
            WindowsPlatformCamera.SetActive(true);
        }
    }
}
