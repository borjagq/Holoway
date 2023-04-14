using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalNetworkSettings
{
    private String IPAddress;
    private int Port;
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
}
