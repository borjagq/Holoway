using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterMovementScript : NetworkBehaviour
{
    [Header("Helper Objects")]
    public GameObject ForwardObject;
    public GameObject PlayerCamera;

    

    [Header("Camera Related Objects - Common")]
    public GameObject CameraSocket;
    public GameObject CameraHolder;
    public GameObject DummyCameraHolder;
    public GameObject CameraDirectionVector;
    public float DistanceFactor = 2.0f;
    public float ScrollFactor = 3.0f;

    [Header("Animations")]
    public ClientNetworkAnimator Animator;
    [Header("Variables")]
    public float WalkSpeedMultiplier = 1.0f;
    public float WalkDirectionMultiplier = 20.0f;
    public float SprintSpeedMultiplier = 5.0f;
    public float SprintDirectionMultiplier = 40.0f;

    public bool FirstPersonCameraEnabled = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return; // Only controlled by the client
        Cursor.visible = false;
        Animator.Animator.SetFloat("Speed", Input.GetAxis("Vertical"));
        Animator.Animator.SetFloat("Direction", Input.GetAxis("Horizontal"));
        Animator.Animator.SetBool("Sprint", Input.GetKey(KeyCode.LeftShift));

        float ForwardMultiplier = WalkSpeedMultiplier;
        float DirectionMultiplier = WalkDirectionMultiplier;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ForwardMultiplier = SprintSpeedMultiplier;
            DirectionMultiplier = SprintDirectionMultiplier;
        }
        else
        {
            ForwardMultiplier = WalkSpeedMultiplier;
            DirectionMultiplier = WalkDirectionMultiplier;
        }
        this.transform.Rotate(new Vector3(0.0f, Input.GetAxis("Vertical") * Input.GetAxis("Horizontal") * Time.deltaTime * DirectionMultiplier, 0.0f));
        Vector3 ForwardVector = ForwardObject.transform.position - this.transform.position;
        if (Input.GetAxis("Vertical") > 0)
        {
            this.transform.position += ForwardVector * Input.GetAxis("Vertical") * ForwardMultiplier * Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            FirstPersonCameraEnabled = !FirstPersonCameraEnabled;
            if(FirstPersonCameraEnabled)
            {
                PlayerCamera.transform.position = CameraHolder.transform.position;
            }
            else
            {
                PlayerCamera.transform.position = CameraDirectionVector.transform.position;
            }
        }
        //================================================================================================
        //              CAMERA ROTATION SCRIPT
        //================================================================================================
        if (!FirstPersonCameraEnabled)
        {
            Vector3 CamVector = CameraSocket.transform.position - CameraDirectionVector.transform.position;
            PlayerCamera.transform.position = CameraHolder.transform.position - DistanceFactor * CamVector;
            DistanceFactor -= Input.mouseScrollDelta.y * ScrollFactor * Time.deltaTime;
        }
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        CameraHolder.transform.Rotate(new Vector3(mouseY, 0.0f, 0.0f));
        CameraSocket.transform.Rotate(new Vector3(0.0f, mouseX, 0.0f));

    }
    public void OnDrawGizmos()
    {
        
//        Gizmos.DrawRay(new Ray(CameraDirectionVector.transform.position, CamVector.normalized));
    }
}
