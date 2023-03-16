using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using TMPro;

public class AvatarModificationRoomTests : InputTestFixture
{
    [SetUp]
    public override void Setup()
    {
        SceneManager.LoadScene(6);
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
    public IEnumerator Test_CheckUICanvas()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterModificationPanel()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/CharacterModificationPanel");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterModificationHandler()
    {
        yield return null;
        GameObject Object = GameObject.Find("CharacterModificationHandler");
        Assert.NotNull(Object);
    }
    //================================================================
    //  TESTS FOR: Labels
    //================================================================
    [UnityTest]
    public IEnumerator Test_CharacterGenderSelectionLabel()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/CharacterModificationPanel/Labels/Label_Gender");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterShirtSelectionLabel()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/CharacterModificationPanel/Labels/Label_Shirt");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterPantSelectionLabel()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/CharacterModificationPanel/Labels/Label_Pant");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterShoeSelectionLabel()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/CharacterModificationPanel/Labels/Label_Shoe");
        Assert.NotNull(Object);
    }
    //================================================================
    //  TESTS FOR: Inputs
    //================================================================
    [UnityTest]
    public IEnumerator Test_CharacterGenderSelectionDropdown()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/CharacterModificationPanel/Inputs/Dropdown_Gender");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterShirtSelectionDropdown()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/CharacterModificationPanel/Inputs/Dropdown_Shirt");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterPantSelectionDropdown()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/CharacterModificationPanel/Inputs/Dropdown_Pant");
        Assert.NotNull(Object);
    }
    [UnityTest]
    public IEnumerator Test_CharacterShoeSelectionDropdown()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/CharacterModificationPanel/Inputs/Dropdown_Shoe");
        Assert.NotNull(Object);
    }
    //================================================================
    //  TESTS FOR: Input Values
    //================================================================
    [UnityTest]
    public IEnumerator Test_CharacterGenderSelectionDropdownValues()
    {
        yield return null;
        GameObject Object = GameObject.Find("UICanvas/CharacterModificationPanel/Inputs/Dropdown_Gender");
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
    
}
