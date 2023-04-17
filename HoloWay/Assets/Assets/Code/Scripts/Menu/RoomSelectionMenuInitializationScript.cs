using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelectionMenuInitializationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalGameSettings.Instance.NetworkSettings.SetNetworkType(NetworkType.Host);
    }
}
