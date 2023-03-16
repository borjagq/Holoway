using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConnectPlayer : MonoBehaviour
{
    public static int num_of_players_connected = 0;
    public static void connect()
    {
       
    }
    

    private Button startServerButton;
    

    private Button startClientButton;
    
  
    private Button startHostButton;

    private void Start()
    {
        //startServerButton.onClick.AddListener(() =>
        //{
        //   if (NetworkManager.Singleton.StartHost())
        //    {
        //        Logger.Instance.LogInfo("Server Started");
        //    }
        //   else
        //    {
        //        Logger.Instance.LogInfo("Server not Started");
        //    }
        //});
        //startClientButton.onClick.AddListener(() =>
        //{
        //    if (NetworkManager.Singleton.StartClient())
        //    {
        //        Logger.Instance.LogInfo("Client Started");
        //    }
        //    else
        //    {
        //        Logger.Instance.LogInfo("Client not Started");
        //    }
        //});

        //startHostButton.onClick.AddListener(() =>
        //{

        //    if (NetworkManager.Singleton.StartHost())
        //    {
        //        Logger.Instance.LogInfo("Host Started");
        //    }
        //    else
        //    {
        //        Logger.Instance.LogInfo("Host  has not  Started");
        //    }

        //});
    }
        
}
