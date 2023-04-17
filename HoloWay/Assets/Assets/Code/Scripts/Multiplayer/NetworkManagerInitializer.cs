using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkManagerInitializer : MonoBehaviour
{

    void Start()
    {
        GlobalGameSettings.Instance.NetworkSettings.InitializeNetworkManager(NetworkManager.Singleton);
        //GlobalNetworkDetails.InitializeNetworkManager(NetworkManager.Singleton);   
    }
}
