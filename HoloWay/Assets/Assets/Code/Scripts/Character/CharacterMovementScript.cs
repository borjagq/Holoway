using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class CharacterMovementScript : NetworkBehaviour
{
    [Header("Helper Objects")]
    public GameObject ForwardObject;
    public GameObject PlayerCamera;
    public GameObject UMARenderer;
    [Header("Pause Menu")]
    public GameObject PauseMenuObject;
    public bool IsInPauseMenu = false;

    [Header("HUD Menu")]
    public GameObject HudObject;
    public TMP_Text Microphone_Text;

    [Header("Camera Related Objects - Common")]
    public GameObject CameraSocket;
    public GameObject CameraHolder;
    public GameObject DummyCameraHolder;
    public GameObject CameraDirectionVector;
    public float DistanceFactor = 2.0f;
    public float ScrollFactor = 3.0f;

    [Header("Controls - Mouse Related Settings")]
    public float MouseSensitivityX;
    public float MouseSensitivityY;

    [Header("Animations")]
    public ClientNetworkAnimator Animator;
    [Header("Variables")]
    public float WalkSpeedMultiplier = 1.0f;
    public float WalkDirectionMultiplier = 20.0f;
    public float SprintSpeedMultiplier = 5.0f;
    public float SprintDirectionMultiplier = 40.0f;

    public bool FirstPersonCameraEnabled = false;

    [Header("Reference Scripts")]
    public VoiceHandler VoiceHandlerScript;

    void Start()
    {
        if(!IsOwner)
        {
            PlayerCamera.SetActive(false);
        }
        VoiceHandlerScript = this.GetComponent<VoiceHandler>();

        GlobalGameSettings.Instance.LoadSettings();
        
        GlobalGameSettings.Instance.GameState.SetGameState(GameState.InGame);
        Cursor.visible = false;

        Transform[] children = this.gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform t in children)
        {
            if(t.name == "UMARenderer")
            {
                UMARenderer = t.gameObject;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!IsOwner) return; // Only controlled by the client
        if(this.VoiceHandlerScript != null)
        {
            if(this.VoiceHandlerScript.PublishingMode)
            {
                Microphone_Text.text = "Microphone: On";
            }
            else
            {
                Microphone_Text.text = "Microphone: Off";
            }
        }
        MouseSensitivityX = GlobalGameSettings.Instance.ControlSettings.MouseSensitivityX;
        MouseSensitivityY = GlobalGameSettings.Instance.ControlSettings.MouseSensitivityY;
        MovePlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetKey(KeyCode.LeftShift));
        if (Input.GetKeyDown(KeyCode.C))
        {
            FirstPersonCameraEnabled = !FirstPersonCameraEnabled;
            if(FirstPersonCameraEnabled)
            {
                PlayerCamera.transform.position = CameraHolder.transform.position;
                //UMARenderer.layer = LayerMask.NameToLayer("FPSCameraObject");
            }
            else
            {
                PlayerCamera.transform.position = CameraDirectionVector.transform.position;
                //UMARenderer.layer = LayerMask.NameToLayer("Default");
            }
        }
        RotateCamera(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
            );
        //================================================================================================
        //             KEY PRESS SCRIPT
        //================================================================================================
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GlobalGameSettings.Instance.GameState.GetGameState() == GameState.InGame)
            {
                Cursor.visible = !Cursor.visible;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            IsInPauseMenu = !IsInPauseMenu;
            Cursor.visible = IsInPauseMenu;
            PauseMenuObject.SetActive(IsInPauseMenu);
            if (IsInPauseMenu)
            {
                GlobalGameSettings.Instance.GameState.SetGameState(GameState.InMenu);
                HudObject.SetActive(false);
            }
            else
            {
                GlobalGameSettings.Instance.GameState.SetGameState(GameState.InGame);
                HudObject.SetActive(true);
            }
        }
    }

    public void MovePlayer(float InputX, float InputY, bool Sprint)
    {
        float ForwardMultiplier = WalkSpeedMultiplier;
        float DirectionMultiplier = WalkDirectionMultiplier;
        if (Sprint)
        {
            ForwardMultiplier = SprintSpeedMultiplier;
            DirectionMultiplier = SprintDirectionMultiplier;
        }
        else
        {
            ForwardMultiplier = WalkSpeedMultiplier;
            DirectionMultiplier = WalkDirectionMultiplier;
        }
        Animator.Animator.SetFloat("Speed", InputY);
        Animator.Animator.SetFloat("Direction", InputX);
        Animator.Animator.SetBool("Sprint", Sprint);
        if (!Cursor.visible)
        {
            if (InputY > 0.0f)
            {
                this.transform.Rotate(new Vector3(0.0f, InputY * InputX * Time.deltaTime * DirectionMultiplier, 0.0f));
            }
            Vector3 ForwardVector = ForwardObject.transform.position - this.transform.position;
            if (InputY > 0)
            {
                this.transform.position += ForwardVector * InputY* ForwardMultiplier * Time.deltaTime;
            }
        }
    }
    public void RotateCamera(float mouseX, float mouseY)
    {
        //================================================================================================
        //              CAMERA ROTATION SCRIPT
        //================================================================================================
        if (!FirstPersonCameraEnabled)
        {
            Vector3 CamVector = CameraSocket.transform.position - CameraDirectionVector.transform.position;
            PlayerCamera.transform.position = CameraHolder.transform.position - DistanceFactor * CamVector;
            DistanceFactor -= Input.mouseScrollDelta.y * ScrollFactor * Time.deltaTime;
        }
        if (!Cursor.visible)
        {
            /*float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");*/
            CameraHolder.transform.Rotate(new Vector3(mouseY * MouseSensitivityY / 50.0f, 0.0f, 0.0f));
            CameraSocket.transform.Rotate(new Vector3(0.0f, mouseX * MouseSensitivityX / 50.0f, 0.0f));
        }
    }
    public void DetectKeyPresses()
    {
    }
}
