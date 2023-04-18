using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerSideScript : NetworkBehaviour
{
    public bool Initialized = false;

    public bool ServerHasDefaultSpawnPosition = true;
    public GameObject DefaultSpawnPosition;

    public NetworkManager NetManager;
    // Start is called before the first frame update
    void Start()
    {
        NetManager = NetworkManager.Singleton;
        
    }

    private void Singleton_OnClientDisconnectCallback(ulong obj)
    {
        throw new System.NotImplementedException();
    }

    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        if(NetManager.ConnectedClients.ContainsKey(obj))
        {
            var client = NetManager.ConnectedClients[obj];
            NetworkObject ClientPlayer = client.PlayerObject;
            if(ClientPlayer != null)
            {
                Debug.Log($"Spawning {obj} on default spawn location: " + DefaultSpawnPosition.transform.position);
                if (ServerHasDefaultSpawnPosition) {
                    ClientPlayer.transform.position = DefaultSpawnPosition.transform.position;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Initialized)
        {
            if (IsServer || IsHost)
            {
                Debug.Log("Attaching callbacks..");
                NetManager.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
                NetManager.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
                if(IsHost)
                {
                    Singleton_OnClientConnectedCallback(0);
                }
                Initialized = true;

            }
        }
    }


}
