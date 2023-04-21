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
        if (!IsOwner) return;
        Debug.Log("Called On NetworkSpawn");
        NetworkScript = this.gameObject.GetComponent<CharacterNetworkingScript>();
        //NetworkClient client = this.GetCurrentNetworkClient();
        //NetworkScript = this.GetNetworkingScript(client);
        Debug.Log("Network Script:");

        
        if (NetworkScript != null)
        {
            NetworkScript.Avatar.LoadFromRecipeString(PlayerPrefs.GetString("AvatarData"));
            NetworkScript.ChangePlayerNameServerRpc(GlobalGameSettings.Instance.SessionSettings.GetSessionName());
            NetworkScript.AvatarRecipeChangeServerRpc(NetworkScript.Avatar.GetCurrentRecipe());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
    }
    public NetworkClient GetCurrentNetworkClient()
    {
        ulong clientId = NetworkManager.Singleton.LocalClientId;
        Debug.Log(clientId);
        if(!NetworkManager.Singleton.ConnectedClients.TryGetValue(clientId, out NetworkClient client))
        {
            return null;
        }
        Debug.Log("Network Client: ");
        Debug.Log(client);
        return client;
    }
    public CharacterNetworkingScript GetNetworkingScript(NetworkClient client)
    {
        if (!client.PlayerObject.TryGetComponent<CharacterNetworkingScript>(out var networkingScript))
        {
            return null;
        }
        Debug.Log("Networking script:");
        Debug.Log(networkingScript);
        return networkingScript;
    }
}
