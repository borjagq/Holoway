using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterMovementScript : NetworkBehaviour
{
    [Header("Helper Objects")]
    public GameObject ForwardObject;
    public GameObject PlayerCamera;
    [Header("Camera Related Objects - First Person")]
    public GameObject FirstPersonCameraSocket;
    public GameObject FirstPersonCameraHolder;

    [Header("Camera Related Objects - Third Person")]
    public GameObject ThirdPersonCameraSocket;
    public GameObject ThirdPersonCameraHolder;
    public GameObject ThirdPersonCameraHolderPosition;

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
                Debug.Log("Enabled First Person Camera");
                PlayerCamera.transform.position = FirstPersonCameraHolder.transform.position;
                PlayerCamera.transform.parent = FirstPersonCameraHolder.transform;
            }
            else
            {
                Debug.Log("Enabled Third Person Camera");
                //PlayerCamera.transform.position = ThirdPersonCameraHolder.transform.position;
                PlayerCamera.transform.position = ThirdPersonCameraHolderPosition.transform.position;
                PlayerCamera.transform.parent = ThirdPersonCameraHolder.transform;
            }
        }
        //================================================================================================
        //              CAMERA ROTATION SCRIPT
        //================================================================================================
        if(FirstPersonCameraEnabled)
        {
            // If the camera is first person then we rotate the player along with the camera or just rotate the player
            // This will also rotate the camera
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            FirstPersonCameraHolder.transform.Rotate(new Vector3(mouseY, 0.0f, 0.0f));
            FirstPersonCameraSocket.transform.Rotate(new Vector3(0.0f, mouseX, 0.0f));

        }
        else
        {
            // If the camera is third person we will only rotate the camera, when we press W key it will automatically
            // detect the direction camera is pointing and rotate the player...
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            ThirdPersonCameraHolder.transform.Rotate(new Vector3(mouseY, 0.0f, 0.0f));
            ThirdPersonCameraSocket.transform.Rotate(new Vector3(0.0f, mouseX, 0.0f));

        }

    }
}
