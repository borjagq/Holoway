using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public enum NetworkType
{
    None = 0,
    Client = 1,
    Server = 2,
    Host = 3
}

public class GlobalNetworkDetails : MonoBehaviour
{
    public static NetworkType gNetworkType;
    public static void InitializeNetworkManager(NetworkManager manager)
    {
        switch (GlobalNetworkDetails.gNetworkType)
        {
            case NetworkType.Client:
                {
                    //manager.NetworkConfig.NetworkTransport.
                    manager.StartClient();
                    break;
                }
            case NetworkType.Host:
                {
                    manager.StartHost();
                    break;
                }
            case NetworkType.Server:
                {
                    manager.StartServer();
                    break;
                }
        }
    }
}
