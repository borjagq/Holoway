using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraVIew : MonoBehaviour
{
    [SerializeField]
        private GameObject camObj;
    [SerializeField]
        private Camera cam;
    [SerializeField]
        private GameObject EditButton;
    void Update(){
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {   
                if(hit.collider.name == EditButton.name) {
                    Debug.Log("test");
                    DesactivateAllCam();
                }


            }
        }   
       
    }

    public void DesactivateAllCam() {
        foreach (Camera camera in Camera.allCameras) {
            camera.enabled = false;
            camObj.SetActive(true);
            cam.enabled  = true;
        }
        
    }
}
