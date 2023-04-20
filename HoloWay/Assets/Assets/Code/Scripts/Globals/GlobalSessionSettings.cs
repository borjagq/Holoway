using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSessionSettings
{
    private string SessionName;
    
    public GlobalSessionSettings()
    {
        Debug.Log("Initializing Sessions...");
    }

    public void SetSessionName(string SessionName)
    {
        Debug.Log("Setting the name to: " + SessionName);
        this.SessionName = SessionName;
    }
    public string GetSessionName()
    {
        return this.SessionName;
    }
}
