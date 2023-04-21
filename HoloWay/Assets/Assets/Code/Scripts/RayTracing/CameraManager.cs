using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject RayTracingCam;


    private bool RayTracacingEnable = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey ("e")) {
            RayTracacingEnable = ! RayTracacingEnable;
        }

        if (RayTracacingEnable) {
            RayTracingCam.GetComponent<RayTracingMaster>().enabled = true;

        } else {
            RayTracingCam.GetComponent<RayTracingMaster>().enabled = false;

        }
    }
}
