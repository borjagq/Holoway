using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class AvatarModificationRoomTests : InputTestFixture
{

    [SetUp]
    public override void Setup()
    {
        SceneManager.LoadScene(3);
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
    public IEnumerator Test_CheckChangingRoomBase()
    {
        yield return null;
        GameObject Object = GameObject.Find("Changing Room");
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
    public IEnumerator Test_CheckUIMenu1()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menu1");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_CharacterModificationRightMenu()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu1/UICanvas3D_Menu1_RightMenu");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterModificationLeftMenu()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu1/UICanvas3D_Menu1_LeftMenu");
        Assert.NotNull(Object);
    }

    //================================================================
    //  TESTS FOR: Buttons
    //================================================================
    [UnityTest]
    public IEnumerator Test_CharacterGenderDropdown()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu1/UICanvas3D_Menu1_LeftMenu/Dropdown_Gender");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterHeadButton()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu1/UICanvas3D_Menu1_RightMenu/Button_Head");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterTorsoButton()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu1/UICanvas3D_Menu1_RightMenu/Button_Torso");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterPantsButton()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu1/UICanvas3D_Menu1_RightMenu/Button_Pants");
        Assert.NotNull(Object);
    }

    [UnityTest]
    public IEnumerator Test_CharacterShoesButton()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu1/UICanvas3D_Menu1_RightMenu/Button_Shoes");
        Assert.NotNull(Object);
    }

    //================================================================
    //  TESTS FOR: Button Values
    //================================================================
    [UnityTest]
    public IEnumerator Test_CharacterGenderSelectionDropdownValues()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu1/UICanvas3D_Menu1_LeftMenu/Dropdown_Gender");
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
        GameObject Object = GameObject.Find("Menus/Menu1/UICanvas3D_Menu1_RightMenu/Button_Head/Text (TMP)");
        TMP_Text btn_Text = Object.GetComponent<TMP_Text>();
        Assert.AreEqual("Head", btn_Text.text);
    }

    [UnityTest]
    public IEnumerator Test_CharacterTorsoButtonValue()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu1/UICanvas3D_Menu1_RightMenu/Button_Torso/Text (TMP)");
        TMP_Text btn_Text = Object.GetComponent<TMP_Text>();
        Assert.AreEqual("Torso", btn_Text.text);
    }

    [UnityTest]
    public IEnumerator Test_CharacterPantsButtonValue()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu1/UICanvas3D_Menu1_RightMenu/Button_Pants/Text (TMP)");
        TMP_Text btn_Text = Object.GetComponent<TMP_Text>();
        Assert.AreEqual("Pants", btn_Text.text);
    }

    [UnityTest]
    public IEnumerator Test_CharacterShoesButtonValue()
    {
        yield return null;
        GameObject Object = GameObject.Find("Menus/Menu1/UICanvas3D_Menu1_RightMenu/Button_Shoes/Text (TMP)");
        TMP_Text btn_Text = Object.GetComponent<TMP_Text>();
        Assert.AreEqual("Shoes", btn_Text.text);
    }



    //================================================================
    //  TESTS FOR: Change Gender
    //================================================================

    /*
    //DOESNT WORK YET
    [UnityTest]
    public IEnumerator Test_CharacterChangeGenderToMale()
    {
        yield return null;

        GameObject GUIElement = GameObject.Find("CharacterModificationScript");
        AvatarModificationMenuScript CharacterAnim = GUIElement.GetComponent<AvatarModificationMenuScript>();

        CharacterAnim.Dropdown_Gender_OnChange(0);

        yield return new WaitForSeconds(2.5f);

        string RaceName = CharacterAnim.GetRaceName();

        Assert.AreEqual("HumanMaleHighPoly", RaceName);
    }

    

    [UnityTest]
    public IEnumerator Test_CharacterChangeGenderToFemale()
    {
        yield return null;

        GameObject GUIElement = GameObject.Find("CharacterModificationScript");
        AvatarModificationMenuScript CharacterAnim = GUIElement.GetComponent<AvatarModificationMenuScript>();

        CharacterAnim.Dropdown_Gender_OnChange(1);

        yield return new WaitForSeconds(2.5f);

        string RaceName = CharacterAnim.GetRaceName();

        Assert.AreEqual("HumanFemaleHighPoly", RaceName);
    }
    */


    //================================================================
    //  TESTS FOR: Change Camera Focus
    //================================================================

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

    
    //DOESNT WORK YET
    [UnityTest]
    public IEnumerator Test_ChangeCameraEscapes()
    {
        yield return null;

        GameObject GUIElement = GameObject.Find("CharacterModificationScript");
        AvatarModificationMenuScript CharacterAnim = GUIElement.GetComponent<AvatarModificationMenuScript>();

        GameObject Object = GameObject.Find("CameraPositions/ShoeFocus");

        CharacterAnim.FocusCameraToBodyPart(Object);
        
        yield return new WaitForSeconds(2);

        CharacterAnim.simulatePress = true;
        GameObject FocusObject = CharacterAnim.GetCurrentTarget();

        yield return new WaitForSeconds(2);

        Assert.AreEqual(Object, FocusObject);
    }
   
}
