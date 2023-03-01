using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelectionMenuScript : MonoBehaviour
{
    public int selectedRoomId;
    public void SelectRoom(int id)
    {
        selectedRoomId = id;
    }
    public int GetSelectedRoomId()
    {
        return selectedRoomId;
    }
    public void SelectRoomOneOnClick()
    {
        this.SelectRoom(1);
    }
    public void SelectRoomTwoOnClick()
    {
        this.SelectRoom(2);
    }
    public void SelectRoomThreeOnClick()
    {
        this.SelectRoom(3);
    }
}
