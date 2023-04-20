using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public enum NetworkType
{
    None = 0,
    Client = 1,
    Server = 2,
    Host = 3
}

public class GlobalNetworkSettings
{
    private String IPAddress;
    private int Port;

    private NetworkType NetType;
    public String GetIPAddress()
    {
        return this.IPAddress;
    }
    public void SetIPAddress(String IPAddress)
    {
        this.IPAddress = IPAddress;
    }
    public int GetPort()
    {
        return this.Port;
    }
    public void SetPort(int port)
    {
        this.Port = port;
    }
    public NetworkType GetNetType() { 
        return this.NetType;
    }
    public void SetNetworkType(NetworkType netType)
    {
        this.NetType = netType;
    }
    public void InitializeNetworkManager(NetworkManager manager)
    {
        switch (this.NetType)
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
    public void ShutdownNetworkManager()
    {
        NetworkManager.Singleton.Shutdown();
    }

}
