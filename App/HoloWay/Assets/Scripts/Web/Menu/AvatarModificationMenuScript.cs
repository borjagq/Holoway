using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UMA.CharacterSystem;
using UnityEngine;
using UnityEngine.UI;

public class AvatarModificationMenuScript : MonoBehaviour
{
    public String[] RaceName = new String[2];
    [Header("UMA specific attributes")]
    public DynamicCharacterAvatar Avatar;
    [Header("Menus")]
    public List<GameObject> MenuObjects = new List<GameObject>();

    [Header("Inputs - Menu 1")]
    [Header("Dropdowns")]
    public TMP_Dropdown GenderDropdown;
    [Header("Buttons")]
    public Button HeadButton;
    public Button TorsoButton;
    public Button LegButton;
    public Button ShoeButton;
    [Header("Focus Points")]
    public GameObject HeadFocusPoint;
    public GameObject TorsoFocusPoint;
    public GameObject LegFocusPoint;
    public GameObject ShoeFocusPoint;
    [Header("Game related")]
    public Camera MainCamera;
    [Header("Variables")]
    public float LerpFactor = 1.0f;
    public float MinCamTargetDistance = 0.01f;
    public bool UseHighPolyModels = true;

    private Vector3 _OldCameraPosition;
    private Vector3 _TargetPosition;
    private bool _IsCameraFocused = false;
    private bool _IsCameraMoving = false;
    private float _StartTime;
    private int _CurrentMenuIndex = 0;
    //[Header("Dropdowns")]
    public void Start()
    {
        
        RaceName[0] = "HumanMale";
        RaceName[1] = "HumanFemale";
        if (UseHighPolyModels)
        {
            RaceName[0] += "HighPoly";
            RaceName[1] += "HighPoly";
        }
        _OldCameraPosition = MainCamera.transform.position;
    }
    public void Update()
    {
        if(_IsCameraMoving) { 
            if(Vector3.Distance(MainCamera.transform.position, _TargetPosition) > MinCamTargetDistance)
            {
                float t = (Time.time - _StartTime) * LerpFactor;
                MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, _TargetPosition, t);
            }
            else
            {
                _IsCameraMoving = false;

            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && _IsCameraFocused)
        {
            _TargetPosition = _OldCameraPosition;
            _IsCameraMoving = true;
            _IsCameraFocused = false;
            _StartTime = Time.time;
            if (_CurrentMenuIndex > 0)
            {
                _CurrentMenuIndex--;
                EnableMenu(_CurrentMenuIndex);
            }
        }
    }
    public void Dropdown_Gender_OnChange(Int32 DropdownValue)
    {
        Avatar.activeRace.name = RaceName[GenderDropdown.value];
        Avatar.ChangeRace(RaceName[GenderDropdown.value], true);
        Debug.Log(GenderDropdown.value);
    }
    public void EnableMenu(int menu_index)
    {
        for(int i = 0; i < MenuObjects.Count; i++)
        {
            if (i == _CurrentMenuIndex)
            {
                MenuObjects[i].SetActive(true);
            }
            else
            {
                MenuObjects[i].SetActive(false);
            }
        }
    }
    public void FocusCameraToBodyPart(GameObject target)
    {
        _StartTime = Time.time;
        _IsCameraMoving = true;
        //MainCamera.transform.position = target.transform.position;
        _TargetPosition = target.transform.position;
        _IsCameraFocused = true;
        _CurrentMenuIndex++;
        EnableMenu(_CurrentMenuIndex);
    }
}
