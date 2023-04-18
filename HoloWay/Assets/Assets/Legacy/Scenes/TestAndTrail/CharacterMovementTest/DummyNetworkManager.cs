using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DummyNetworkManager : NetworkManager
{
    public void Start()
    {
        this.StartHost();
    }
}
