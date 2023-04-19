using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetworkManagerInitializer : MonoBehaviour
{

    void Start()
    {
        UnityTransport Transport = NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();
        Transport.ConnectionData.Address = GlobalGameSettings.Instance.NetworkSettings.GetIPAddress();
        Transport.ConnectionData.Port = (ushort) GlobalGameSettings.Instance.NetworkSettings.GetPort();
        GlobalGameSettings.Instance.NetworkSettings.InitializeNetworkManager(NetworkManager.Singleton);
        //GlobalNetworkDetails.InitializeNetworkManager(NetworkManager.Singleton);   
    }
}
