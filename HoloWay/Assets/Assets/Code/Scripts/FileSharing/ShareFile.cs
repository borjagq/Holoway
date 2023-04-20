using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static ID;

public class ShareFile : MonoBehaviour
{
    [SerializeField]
    private Material FileisReadyMaterial;
    
    public static string SharedFileId;
    public static string SharedFileName;
    public static bool fileIsShared = false;
    public void shareFile() {
        // Disable Canvas and empty objects
        GameObject.Find("Canvas").SetActive(false);
        // Change Material of the other sphere
        GameObject.Find("GetFileInterface").GetComponent<MeshRenderer>().material = FileisReadyMaterial;
        // update Global variable with the file name/ID
        SharedFileId = gameObject.GetComponent<ID>().id;
        SharedFileName = gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text;
        fileIsShared = true;
    }

}
