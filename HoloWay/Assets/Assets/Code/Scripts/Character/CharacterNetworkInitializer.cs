using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterNetworkInitializer : NetworkBehaviour
{
    public CharacterNetworkingScript NetworkScript;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Called On NetworkSpawn");
        NetworkScript = this.gameObject.GetComponent<CharacterNetworkingScript>();
        if (NetworkScript != null)
        {
            NetworkScript.ChangePlayerNameServerRpc(GlobalGameSettings.Instance.SessionSettings.GetSessionName());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Debug.ClearDeveloperConsole();
        
    }
}
