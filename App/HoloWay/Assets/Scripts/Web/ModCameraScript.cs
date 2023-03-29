using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModCameraScript : MonoBehaviour
{
    public Camera MainCamera;
    public Transform CameraOriginalLocation;
    public Transform SpineModCameraLocation;
    public Transform CameraTargetLocation;
    public float startTime;
    public float Distance;
    public bool IsViewingMod = false;
    public bool IsCameraMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GetComponent<Camera>();
        GameObject CameraCopy = Instantiate(new GameObject());
        CameraCopy.transform.position = MainCamera.transform.position;
        CameraOriginalLocation= CameraCopy.transform;
        //CameraOriginalLocation = MainCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsCameraMoving)
        {
            float t = (Time.time - startTime) / Distance;
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, CameraTargetLocation.position, t);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startTime = Time.time;
            if(IsViewingMod)
            {
                IsViewingMod = false;
                Distance = Vector3.Distance(MainCamera.transform.position, CameraOriginalLocation.transform.position);
                CameraTargetLocation = CameraOriginalLocation;
            }
            else
            {
                IsViewingMod = true;
                Distance = Vector3.Distance(MainCamera.transform.position, SpineModCameraLocation.transform.position);
                CameraTargetLocation = SpineModCameraLocation;
            }
            IsCameraMoving = true;
        }
    }
}
