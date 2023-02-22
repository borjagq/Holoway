using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public GameObject CameraMainHolder;
    public GameObject CameraHolder;
    public Camera Camera;
    public GameObject FirstPersonCameraMainHolder;

    public GameObject PlayerObject;
    public GameObject Forward;

    public Vector3 CameraMainHolderOldPosition;
    public Vector3 CameraOldPosition;

    private Transform CameraMainHolderOldTransform;
    private Transform CameraHolderOldTransform;

    public bool IsInFPSMode = false;

    public bool IsCursorLocked = true;

    public Animator animator;
    public float MovementSpeed = 5.0f;
    public float RotationSpeed = 5.0f;
    public float Gravity = 0.0098f;
    public CharacterController CharacterController;
    // Start is called before the first frame update
    void Start()
    {

        CameraMainHolderOldTransform = CameraMainHolder.transform;
        CameraMainHolderOldPosition = CameraMainHolder.transform.localPosition;
        CameraOldPosition = Camera.transform.localPosition;
        CameraHolderOldTransform = CameraHolder.transform;
        if (IsCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        animator = GetComponent<Animator>();
        CharacterController= GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = Vector3.zero;
        if (!CharacterController.isGrounded)
        {
            moveVector += Physics.gravity * Time.deltaTime;

        }
        animator.SetFloat("Walking", Input.GetAxis("Vertical"));
        animator.SetFloat("Turning", Input.GetAxis("Horizontal"));
        moveVector += -(this.transform.position - Forward.transform.position) * Input.GetAxis("Vertical") * Time.deltaTime * MovementSpeed;
        CharacterController.Move(moveVector);
        this.transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * RotationSpeed);
        if (IsCursorLocked)
        {
            CameraMainHolder.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
            CameraHolder.transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y"));
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsCursorLocked) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
            IsCursorLocked = !IsCursorLocked;
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            if (!IsInFPSMode)
            {
                CameraMainHolder.transform.position = FirstPersonCameraMainHolder.transform.position;
                Camera.transform.position = CameraHolder.transform.position;
                /*PlayerObject.layer = 6;
                Transform[] objects = PlayerObject.GetComponentsInChildren<Transform>();
                foreach (Transform child_object in objects)
                {
                    child_object.gameObject.layer = 6;
                }*/
            }
            else
            {
                CameraMainHolder.transform.localPosition = CameraMainHolderOldPosition;
                Camera.transform.localPosition = CameraOldPosition;
                /*PlayerObject.layer = 0;
                Transform[] objects = PlayerObject.GetComponentsInChildren<Transform>();
                foreach (Transform child_object in objects)
                {
                    child_object.gameObject.layer = 0;
                }*/

            }
            IsInFPSMode = !IsInFPSMode;
        }
    }
}
