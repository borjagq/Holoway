using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomSelectionMenuScript : MonoBehaviour
{
    public void MakeHost()
    {
        GlobalGameSettings.Instance.NetworkSettings.SetNetworkType(NetworkType.Host);
    }
}
