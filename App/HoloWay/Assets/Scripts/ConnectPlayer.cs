using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;


public class ConnectPlayer : MonoBehaviour
{
    public static int num_of_players_connected = 0;
    public static void connect()
    {
       
    }

    [SerializeField]
    private Button startServerButton;

    [SerializeField]
    private Button startClientButton;

    [SerializeField]
    private Button startHostButton;

    private void Start()
    {
        startServerButton.onClick.AddListener(() =>
        {
           if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Server Started");
            }
           else
            {
                Debug.Log("Server not Started");
            }
        });
        startClientButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Client Started");
            }
            else
            {
                Debug.Log("Client not Started");
            }
        });

        startHostButton.onClick.AddListener(() =>
        {

            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Host Started");
            }
            else
            {
                Debug.Log("Host  has not  Started");
            }

        });

    }

}
