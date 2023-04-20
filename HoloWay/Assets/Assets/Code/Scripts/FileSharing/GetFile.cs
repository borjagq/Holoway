using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShareFile;
using static AccessDrive;
using static LoginSession;
using holowayapi;

public class GetFile : MonoBehaviour
{
    
    [SerializeField]
    private Material GetFileInterfaceMaterial;
    [SerializeField]
    private Material ShareFileInterfaceeMaterial;

    private string api_key;
    private string priv_key;

    // Update is called once per frame
    void Update()
    {
        if (CastRay.hasHit("GetFileInterface", Camera.main) && ShareFile.fileIsShared) {
            string userToken = LoginSession.token;
            string userID = LoginSession.user_id;

            string userSharingToken = AccessDrive.userSharingToken;
            string userSharingID = AccessDrive.userSharingID;

            string filesName = ShareFile.SharedFileName;
            string fileId = ShareFile.SharedFileId;

            // Get the API parameters.
            this.api_key = "N7e9vMq3BMmD84XxwUI4Vhq1snt7iBe8";
            this.priv_key = Application.dataPath + "/Assets/Code/Scripts/HolowayAPI/N7e9vMq3BMmD84XxwUI4Vhq1snt7iBe8.xml";

            // Create the api instance and pass the parameters.
            GameObject APIManager = GameObject.Find("APIManager");
            HolowayAPI api = APIManager.AddComponent<HolowayAPI>();
            api.add_params(priv_key, api_key);

            // share file on the person drive
            StartCoroutine(api.share_file(fileId, new List<string> {userID}, userSharingToken, SharedDone));
        }
    }

    void SharedDone(string status, string msg)
    {
        if (status != "success")
            return;
        
        GameObject.Find("GetFileInterface").GetComponent<MeshRenderer>().material = GetFileInterfaceMaterial;
        GameObject.Find("ShareFileInterface").GetComponent<MeshRenderer>().material = ShareFileInterfaceeMaterial;
        ShareFile.fileIsShared = false;

        GameObject Content = GameObject.Find("Content");
        foreach (Transform child in Content.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

}
