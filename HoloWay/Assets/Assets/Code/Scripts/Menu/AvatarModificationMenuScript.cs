using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UMA;
using UMA.CharacterSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AvatarModificationMenuScript : MonoBehaviour
{
    private String[] RaceName = new String[2];
    private INIFile file = new INIFile();
    

    //================================================================================
    //  STATIC VARIABLES
    //================================================================================
    public static Stack<GameObject> MenuStack = new Stack<GameObject>();

    [Header("Variables")]
    public float LerpFactor = 1.0f;
    public float MinCamTargetDistance = 0.01f;
    public bool UseHighPolyModels = true;
    public GameObject DefaultMenu;
    public GameObject BackButtonMenu;
    public String EdittingBodyPart;

    [Header("Color Picker")]
    public TMP_InputField Input_ColorR;
    public TMP_InputField Input_ColorG;
    public TMP_InputField Input_ColorB;


    [Header("UMA specific attributes")]
    public GameObject UMAPlayer;
    public DynamicCharacterAvatar Avatar;
    public UMAData AvatarUmaData;
    public Dictionary<string, int> AvatarUmaSlotDataMap = new Dictionary<string, int>();

    [Header("Inputs - Menu 1")]
    [Header("Dropdowns")]
    public TMP_Dropdown GenderDropdown;
    [Header("Game related")]
    public Camera MainCamera;

    [Header("Aesthetics")]
    public GameObject SavingPanel;

    private Vector3 _OldCameraPosition;
    private Vector3 _TargetPosition;
    private bool _IsCameraFocused = false;
    private bool _IsCameraMoving = false;
    private float _StartTime;
    
    bool _escapeButton = false;

    public GameObject _CurrentTarget;
    public String _CurrentRace;
    public bool simulatePress = false;

    public GameObject MenuColorPicker;

    //[Header("Dropdowns")]
    public void Start()
    {
        //file.LoadFromFile("./Data.ini");
        Avatar = UMAPlayer.GetComponent<DynamicCharacterAvatar>();
        AvatarUmaData = UMAPlayer.GetComponent<UMAData>();
        MenuStack.Push(DefaultMenu);
        RaceName[0] = "HumanMale";
        RaceName[1] = "HumanFemale";
        if (UseHighPolyModels)
        {
            RaceName[0] += "HighPoly";
            RaceName[1] += "HighPoly";
        }
        _OldCameraPosition = MainCamera.transform.position;
        /*        for (int i = 0; i < AvatarUmaData.umaRecipe.sharedColors.Length; i++)
                {

                    Debug.Log(AvatarUmaData.umaRecipe.sharedColors[i].name);
                    *//*Debug.Log(AvatarUmaData.GetSlot(i).slotName);*//*
                }*/

        string recipe = PlayerPrefs.GetString("AvatarData");
        //string recipe = file.IniReadValue("AvatarDetails", "AvatarData");
       
        if (recipe != null)
        {
            if (recipe != "" && recipe.Trim().Length > 0)
            {
                Debug.Log("Loaded recipe: " + recipe);
                Avatar.LoadFromRecipeString(recipe);
            }
        }
        if (Avatar.activeRace.name == RaceName[0]) GenderDropdown.value = 0;
        else if (Avatar.activeRace.name == RaceName[1]) GenderDropdown.value = 1;

    }

    public void RefocusCamera()
    {
        _TargetPosition = _OldCameraPosition;

        GameObject Object = GameObject.Find("Main Camera");
        _CurrentTarget = Object;

        _IsCameraMoving = true;
        _IsCameraFocused = false;
        _StartTime = Time.time;
        
    }

    public bool GetEscapePressed()
    {
        if (simulatePress)
        {
            return true;
        }
        else
        {
            return Input.GetKeyDown(KeyCode.Escape);
        }
    }

    public void SetEscapedCamera()
    {
        if (GoBackward())
        {
            RefocusCamera();
        }
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


        _escapeButton = GetEscapePressed();
        if (_escapeButton && _IsCameraFocused)
        {
            SetEscapedCamera();
        }
    }
    public void Dropdown_Gender_OnChange(Int32 DropdownValue)
    {
        _CurrentRace = RaceName[GenderDropdown.value];
        Avatar.activeRace.name = RaceName[GenderDropdown.value];
        Avatar.ChangeRace(RaceName[GenderDropdown.value], true);
    }
    public void FocusCameraToBodyPart(GameObject target)
    {
        _StartTime = Time.time;
        _IsCameraMoving = true;
        _IsCameraFocused = true;
        _CurrentTarget = target;
        _TargetPosition = _CurrentTarget.transform.position;
        
    }
    public void GUIGoBackwards()
    {
        if (GoBackward())
        {
            RefocusCamera();
        }
    }

    public bool GoBackward()
    {
        MenuStack.Pop().SetActive(false);
        MenuStack.Peek().SetActive(true);
        if (MenuStack.Count > 1)
        {
            
            return false;
        }
        DeactivateBackButton();
        return true;
    }
    public void GoForward(GameObject target)
    {
        MenuStack.Peek().SetActive(false);
        target.SetActive(true);
        MenuColorPicker = target;
        MenuStack.Push(target);
    }

    public void SetMenuLocation(GameObject obj)
    {
        GameObject Menu = GameObject.Find("Menus/Menu_ColorPicker");
        Debug.Log(obj.transform.position);
        Menu.transform.position = obj.transform.position;
    }
    public void SetBackButtonLocation(GameObject obj)
    {
        GameObject Menu = GameObject.Find("Menus/Menu_BackButton");
        Debug.Log(obj.transform.position);
        Menu.transform.position = obj.transform.position;
    }

    public void ActivateBackButton()
    {
        if (!BackButtonMenu.activeSelf)
        {
            BackButtonMenu.SetActive(true);
        }
    }
    public void DeactivateBackButton()
    {
        if (BackButtonMenu.activeSelf)
        {
            BackButtonMenu.SetActive(false);
        }
    }
    public void SetActiveBodyPart(String BodyPartName)
    {
        EdittingBodyPart = BodyPartName;
    }
    public void ActivateColorPicker()
    {
        Input_ColorR.text = ((byte)(Avatar.GetColor(EdittingBodyPart).color.r * 255)).ToString();
        Input_ColorG.text = ((byte)(Avatar.GetColor(EdittingBodyPart).color.g * 255)).ToString();
        Input_ColorB.text = ((byte)(Avatar.GetColor(EdittingBodyPart).color.b * 255)).ToString();
    }
    public void ChangeBodyPartColor()
    {

        byte r = 0, g = 0, b = 0;
        if(Input_ColorR.text != "")
        {
            if (int.Parse(Input_ColorR.text) > 255)
            {
                r = Byte.Parse("255");
                Input_ColorR.text = "255";
            }
            else if (int.Parse(Input_ColorR.text) <= 255)
            {
                r = Byte.Parse(Input_ColorR.text);
            }
        }
        if (Input_ColorG.text != "")
        {
            if (int.Parse(Input_ColorG.text) > 255)
            {
                g = Byte.Parse("255");
                Input_ColorG.text = "255";
            }
            else if (int.Parse(Input_ColorG.text) <= 255)
            {
                g = Byte.Parse(Input_ColorG.text);
            }
        }
        if (Input_ColorB.text != "")
        {
            if (int.Parse(Input_ColorB.text) > 255)
            {
                b = Byte.Parse("255");
                Input_ColorB.text = "255";
            }
            else if (int.Parse(Input_ColorB.text) <= 255)
            {
                b = Byte.Parse(Input_ColorB.text);
            }
        }
        Avatar.SetColor(EdittingBodyPart, new Color((float)r/255.0f,(float)g/255.0f, (float)b /255.0f));
        Avatar.BuildCharacter();
                    
    }
    public void UpdateBodyPartColorFromColorPicker()
    {

    }
    public GameObject GetCurrentTarget()
    {
        return _CurrentTarget;
    }

    public String GetRaceName()
    {
        return _CurrentRace;
    }

    public void SaveCurrentModel()
    {
        Debug.Log("Saving Model...");
        //file.IniWriteValue("AvatarDetails","AvatarData",Avatar.GetCurrentRecipe());
        PlayerPrefs.SetString("AvatarData", Avatar.GetCurrentRecipe());
        SavingPanel.GetComponent<SavingPanelBehaviour>().ShowPanel(2.5f);
    }
    public void ExitMenu()
    {

        SceneManager.LoadScene(1);
    }
 
    public void SaveAndExit()
    {
        this.SaveCurrentModel();
        StartCoroutine(ExitMenuAfterSomeTime());
    }
    public IEnumerator ExitMenuAfterSomeTime()
    {
        yield return new WaitForSeconds(2.5f);
        this.ExitMenu();
    }

}
