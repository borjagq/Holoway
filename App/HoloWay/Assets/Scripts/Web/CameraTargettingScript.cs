using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraTargettingScript : MonoBehaviour
{

    public TMP_Text PreviewText;

    public List<GameObject> OriginalMaterialList = new List<GameObject>();
    public List<GameObject> SelectedMaterialList = new List<GameObject>();
    public Ray hitRay;
    public GameObject CameraForward;
    public GameObject LastHitObject;
    public TMP_Text text;
    public bool WasPrefabCreated = false;
   
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Instantiate(PreviewText);
        text.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ForwardVec = CameraForward.transform.position - this.transform.position;
        ForwardVec = Vector3.Normalize(ForwardVec);
        //ForwardVec = this.transform.position + ForwardVec;
        Ray ray = new Ray(this.transform.position, ForwardVec);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 5.0f, 7))
        {
            //hitRay = new Ray(this.transform.position, hitInfo.transform.position - this.transform.position);
            Debug.Log(hitInfo.transform.name);
            
            text.transform.position = hitInfo.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            
            text.text = hitInfo.transform.name;
            text.transform.gameObject.SetActive(true);
        }
        else
        {
            text.transform.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            

            OriginalMaterialList.Clear();

            Debug.Log("Shooting Ray from camera");
            
        }
    }

    public void OnDrawGizmos()
    {
        if (!hitRay.Equals(null))
        {
        }
    }
}
