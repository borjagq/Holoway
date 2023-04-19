using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AvatarModificationRoomTests
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene(4);
    }

    [UnityTest]
    public IEnumerator Test_CheckMainCamera()
    {
        yield return null;
        GameObject Object = GameObject.Find("Main Camera");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_CheckLight()
    {
        yield return null;
        GameObject Object = GameObject.Find("Directional Light");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_CheckUMAGLIB()
    {
        yield return null;
        GameObject Object = GameObject.Find("UMA_GLIB");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_CheckDefaultPlayer()
    {
        yield return null;
        GameObject Object = GameObject.Find("Player");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_CheckUIMenu()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus");
        Assert.NotNull(Object);
    }


    [UnityTest]
    public IEnumerator Test_CharacterModificationRightMenu()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterModificationLeftMenu()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_LeftMenu");
        Assert.NotNull(Object);
    }

    //================================================================
    //  TESTS FOR: Buttons
    //================================================================
    [UnityTest]
    public IEnumerator Test_CharacterGenderDropdown()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_LeftMenu/Dropdown_Gender");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_CharacterSaveGlobal()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_LeftMenu/Button_Save_Global");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_CharacterExit()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_LeftMenu/Button_Exit");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_CharacterSaveAndExit()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_LeftMenu/Button_SaveAndExit");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_CharacterHeadButton()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu/Button_Head");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterTorsoButton()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu/Button_Torso");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterPantsButton()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu/Button_Pants");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_CharacterShoesButton()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu/Button_Shoes");
        Assert.NotNull(Object);
    }

    //================================================================
    //  TESTS FOR: Button Values
    //================================================================
    [UnityTest]
    public IEnumerator Test_CharacterGenderSelectionDropdownValues()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_LeftMenu/Dropdown_Gender");
        TMP_Dropdown Dropdown = Object.GetComponent<TMP_Dropdown>();
        List<TMP_Dropdown.OptionData> options = Dropdown.options;
        List<string> option_data = new List<string>();
        foreach (var option in options)
        {
            option_data.Add(option.text);
        }
        Assert.AreEqual(option_data.Contains("Male"), true);
        Assert.AreEqual(option_data.Contains("Female"), true);
    }

    [UnityTest]
    public IEnumerator Test_CharacterHeadButtonValue()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu/Button_Head/Text (TMP)");
        TMP_Text btn_Text = Object.GetComponent<TMP_Text>();
        Assert.AreEqual("Head", btn_Text.text);
    }

    [UnityTest]
    public IEnumerator Test_CharacterTorsoButtonValue()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu/Button_Torso/Text (TMP)");
        TMP_Text btn_Text = Object.GetComponent<TMP_Text>();
        Assert.AreEqual("Torso", btn_Text.text);
    }

    [UnityTest]
    public IEnumerator Test_CharacterPantsButtonValue()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu/Button_Pants/Text (TMP)");
        TMP_Text btn_Text = Object.GetComponent<TMP_Text>();
        Assert.AreEqual("Pants", btn_Text.text);
    }

    [UnityTest]
    public IEnumerator Test_CharacterShoesButtonValue()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu/Button_Shoes/Text (TMP)");
        TMP_Text btn_Text = Object.GetComponent<TMP_Text>();
        Assert.AreEqual("Shoes", btn_Text.text);
    }



    //================================================================
    //  TESTS FOR: Change Gender
    //================================================================


    [UnityTest]
    public IEnumerator Test_CharacterChangeGenderToMale()
    {
        yield return null;

        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_LeftMenu/Dropdown_Gender");
        TMP_Dropdown dropdown = Object.GetComponent<TMP_Dropdown>();
        dropdown.value = 1;

        GameObject GUIElement = GameObject.Find("CharacterModificationScript");
        AvatarModificationMenuScript CharacterAnim = GUIElement.GetComponent<AvatarModificationMenuScript>();
        CharacterAnim.Dropdown_Gender_OnChange(dropdown.value);

        yield return new WaitForSeconds(2.5f);

        dropdown.value = 0;
        yield return new WaitForSeconds(2.5f);
        CharacterAnim.Dropdown_Gender_OnChange(dropdown.value);

        string RaceName = CharacterAnim.GetRaceName();

        Assert.AreEqual("HumanMaleHighPoly", RaceName);
    }


    [UnityTest]
    public IEnumerator Test_CharacterChangeGenderToFemale()
    {
        yield return null;

        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_LeftMenu/Dropdown_Gender");
        TMP_Dropdown dropdown = Object.GetComponent<TMP_Dropdown>();
        dropdown.value = 1;

        GameObject GUIElement = GameObject.Find("CharacterModificationScript");
        AvatarModificationMenuScript CharacterAnim = GUIElement.GetComponent<AvatarModificationMenuScript>();
        CharacterAnim.Dropdown_Gender_OnChange(dropdown.value);

        yield return new WaitForSeconds(2.5f);

        string RaceName = CharacterAnim.GetRaceName();

        Assert.AreEqual("HumanFemaleHighPoly", RaceName);
    }


    ////================================================================
    ////  TESTS FOR: Change Camera Focus
    ////================================================================

    [UnityTest]
    public IEnumerator Test_ChangeCameraPositionToHead()
    {
        yield return null;

        GameObject GUIElement = GameObject.Find("CharacterModificationScript");
        AvatarModificationMenuScript CharacterAnim = GUIElement.GetComponent<AvatarModificationMenuScript>();

        GameObject Object = GameObject.Find("CameraPositions/HeadFocus");

        CharacterAnim.FocusCameraToBodyPart(Object);
        GameObject FocusObject = CharacterAnim.GetCurrentTarget();
        yield return new WaitForSeconds(2.5f);

        Assert.AreEqual(Object, FocusObject);
    }

    [UnityTest]
    public IEnumerator Test_ChangeCameraPositionToTorso()
    {
        yield return null;

        GameObject GUIElement = GameObject.Find("CharacterModificationScript");
        AvatarModificationMenuScript CharacterAnim = GUIElement.GetComponent<AvatarModificationMenuScript>();

        GameObject Object = GameObject.Find("CameraPositions/TorsoFocus");

        CharacterAnim.FocusCameraToBodyPart(Object);
        GameObject FocusObject = CharacterAnim.GetCurrentTarget();
        yield return new WaitForSeconds(2.5f);

        Assert.AreEqual(Object, FocusObject);
    }

    [UnityTest]
    public IEnumerator Test_ChangeCameraPositionToLeg()
    {
        yield return null;

        GameObject GUIElement = GameObject.Find("CharacterModificationScript");
        AvatarModificationMenuScript CharacterAnim = GUIElement.GetComponent<AvatarModificationMenuScript>();

        GameObject Object = GameObject.Find("CameraPositions/LegFocus");

        CharacterAnim.FocusCameraToBodyPart(Object);
        GameObject FocusObject = CharacterAnim.GetCurrentTarget();
        yield return new WaitForSeconds(2.5f);

        Assert.AreEqual(Object, FocusObject);
    }

    [UnityTest]
    public IEnumerator Test_ChangeCameraPositionToShoe()
    {
        yield return null;

        GameObject GUIElement = GameObject.Find("CharacterModificationScript");
        AvatarModificationMenuScript CharacterAnim = GUIElement.GetComponent<AvatarModificationMenuScript>();

        GameObject Object = GameObject.Find("CameraPositions/ShoeFocus");

        CharacterAnim.FocusCameraToBodyPart(Object);
        GameObject FocusObject = CharacterAnim.GetCurrentTarget();
        yield return new WaitForSeconds(2.5f);

        Assert.AreEqual(Object, FocusObject);
    }

    //================================================================
    //  TESTS FOR: Escape from Camera Focus
    //================================================================

    [UnityTest]
    public IEnumerator Test_ChangeCameraEscapes()
    {
        yield return null;

        GameObject GUIElement = GameObject.Find("CharacterModificationScript");
        AvatarModificationMenuScript CharacterAnim = GUIElement.GetComponent<AvatarModificationMenuScript>();

        GameObject Object = GameObject.Find("CameraPositions/HeadFocus");

        CharacterAnim.FocusCameraToBodyPart(Object);
        CharacterAnim.GoForward(Object);

        yield return new WaitForSeconds(2.5f);

        CharacterAnim.simulatePress = true;
        CharacterAnim.SetEscapedCamera();
        CharacterAnim.Update();

        GameObject FocusObject = CharacterAnim.GetCurrentTarget();
        GameObject MainCamera = GameObject.Find("Main Camera");

        yield return new WaitForSeconds(2.5f);

        Assert.AreEqual(MainCamera, FocusObject);
    }

    //================================================================
    //  TESTS FOR: Button Clicks
    //================================================================
    [UnityTest]
    public IEnumerator Test_HeadButtonClick()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu/Button_Head");
        Button HeadButton = Object.GetComponent<Button>();
        HeadButton.onClick.Invoke();

        yield return new WaitForSeconds(2.5f);

        GameObject HeadMenu = GameObject.Find("Menus/Menu_Head");
        
        Assert.IsTrue(HeadMenu.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator Test_HairButtonClick()
    {
        yield return Test_HeadButtonClick();

        GameObject Object = GameObject.Find("Menus/Menu_Head/UICanvas3D_Menu2_RightMenu/Button_Hair");
        Button HeadButton = Object.GetComponent<Button>();
        HeadButton.onClick.Invoke();

        yield return new WaitForSeconds(2.5f);

        GameObject ColorPicker = GameObject.Find("Menus/Menu_ColorPicker");

        Assert.IsTrue(ColorPicker.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator Test_SaveHairColorsClick()
    {
        yield return Test_HairButtonClick();
     
        GameObject RedColor = GameObject.Find("Menus/Menu_ColorPicker/UICanvas3D_MenuHair_RightMenu/Input_ColorRed");
        GameObject GreenColor = GameObject.Find("Menus/Menu_ColorPicker/UICanvas3D_MenuHair_RightMenu/Input_ColorGreen");
        GameObject BlueColor = GameObject.Find("Menus/Menu_ColorPicker/UICanvas3D_MenuHair_RightMenu/Input_ColorBlue");

        TMP_InputField RedInput = RedColor.GetComponent<TMP_InputField>();
        TMP_InputField GreenInput = GreenColor.GetComponent<TMP_InputField>();
        TMP_InputField BlueInput = BlueColor.GetComponent<TMP_InputField>();

        RedInput.text = "290";
        GreenInput.text = "290";
        BlueInput.text = "290";

        GameObject Object = GameObject.Find("Menus/Menu_ColorPicker/UICanvas3D_MenuHair_RightMenu/Button_Save");
        Button HeadButton = Object.GetComponent<Button>();
        HeadButton.onClick.Invoke();

        yield return new WaitForSeconds(2.5f);

        Assert.IsNotNull(PlayerPrefs.GetString("AvatarData"));
    }


    [UnityTest]
    public IEnumerator Test_EyeButtonClick()
    {
        yield return Test_HeadButtonClick();

        GameObject Object = GameObject.Find("Menus/Menu_Head/UICanvas3D_Menu2_RightMenu/Button_Eyes");
        Button HeadButton = Object.GetComponent<Button>();
        HeadButton.onClick.Invoke();

        yield return new WaitForSeconds(2.5f);

        GameObject ColorPicker = GameObject.Find("Menus/Menu_ColorPicker");

        Assert.IsTrue(ColorPicker.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator Test_SaveEyeColorsClick()
    {
        yield return Test_EyeButtonClick();

        GameObject Object = GameObject.Find("Menus/Menu_ColorPicker/UICanvas3D_MenuHair_RightMenu/Button_Save");
        Button HeadButton = Object.GetComponent<Button>();
        HeadButton.onClick.Invoke();

        yield return new WaitForSeconds(2.5f);

        Debug.Log(PlayerPrefs.GetString("AvatarData"));

        Assert.IsNotNull(PlayerPrefs.GetString("AvatarData"));
    }


    [UnityTest]
    public IEnumerator Test_TorsoButtonClick()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu/Button_Torso");
        Button HeadButton = Object.GetComponent<Button>();
        HeadButton.onClick.Invoke();

        yield return new WaitForSeconds(2.5f);

        GameObject HeadMenu = GameObject.Find("Menus/Menu_Torso");

        Assert.IsTrue(HeadMenu.activeInHierarchy);
    }


    [UnityTest]
    public IEnumerator Test_PantsButtonClick()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu/Button_Pants");
        Button HeadButton = Object.GetComponent<Button>();
        HeadButton.onClick.Invoke();

        yield return new WaitForSeconds(2.5f);

        GameObject HeadMenu = GameObject.Find("Menus/Menu_Pants");

        Assert.IsTrue(HeadMenu.activeInHierarchy);
    }


    [UnityTest]
    public IEnumerator Test_ShoesButtonClick()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_RightMenu/Button_Shoes");
        Button HeadButton = Object.GetComponent<Button>();
        HeadButton.onClick.Invoke();

        yield return new WaitForSeconds(2.5f);

        GameObject HeadMenu = GameObject.Find("Menus/Menu_Shoes");

        Assert.IsTrue(HeadMenu.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator Test_SaveAndExit()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu_Main/UICanvas3D_Menu1_LeftMenu/Button_SaveAndExit");
        Button HeadButton = Object.GetComponent<Button>();
        HeadButton.onClick.Invoke();

        yield return new WaitForSeconds(3f);

        Assert.AreEqual(1, SceneManager.GetActiveScene().buildIndex);
    }

}
