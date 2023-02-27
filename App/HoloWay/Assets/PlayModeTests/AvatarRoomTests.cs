using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[TestFixture]
public class AvatarRoomTests : InputTestFixture
{
    private Mouse DefaultMouse;
    private Keyboard DefaultKeyboard;
    public override void Setup()
    {
        base.Setup();

        DefaultMouse = InputSystem.AddDevice<Mouse>();
        //DefaultMouse.Setup();
        DefaultKeyboard = InputSystem.AddDevice<Keyboard>();

        //DefaultKeyboard.Setup();
        SceneManager.LoadScene(1);
    }

    [UnityTest]
    public IEnumerator Test_CheckCamera()
    {
        yield return null;
        GameObject CameraObject = GameObject.Find("Main Camera");
        Assert.IsNotNull(CameraObject);
    }

    [UnityTest]
    public IEnumerator Test_CheckEventSystem()
    {
        yield return null;
        GameObject EventSystem = GameObject.Find("EventSystem");
        Assert.IsNotNull(EventSystem);
    }

    [UnityTest]
    public IEnumerator Test_CheckLighting()
    {
        yield return null;
        GameObject EventSystem = GameObject.Find("Point Light");
        Assert.IsNotNull(EventSystem);
    }

    /////CANVAS TESTING///////////////////////////////////////    
    [UnityTest]
    public IEnumerator Test_CheckCanvas()
    {
        yield return null;
        GameObject Canvas = GameObject.Find("Canvas");
        Assert.IsNotNull(Canvas);
    }

    /////CHECK IF BUTTONS EXISTS///////////////////////////
    [UnityTest]
    public IEnumerator Test_CheckScrollView()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas/EmoticonPanel/Scroll View/Scrollbar Vertical/Sliding Area/Handle");
        Assert.IsNotNull(GUIElement);
    }

    [UnityTest]
    public IEnumerator Test_CheckScrollViewWaveButton()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas/EmoticonPanel/Scroll View/Viewport/Content/Button_Wave");
        Assert.IsNotNull(GUIElement);
    }

    [UnityTest]
    public IEnumerator Test_CheckScrollViewShakeHandsButton()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas/EmoticonPanel/Scroll View/Viewport/Content/Button_ShakeHands");
        Assert.IsNotNull(GUIElement);
    }

    [UnityTest]
    public IEnumerator Test_CheckScrollViewSitDownButton()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas/EmoticonPanel/Scroll View/Viewport/Content/Button_SitDown");
        Assert.IsNotNull(GUIElement);
    }

    [UnityTest]
    public IEnumerator Test_CheckScrollViewStandUpButton()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas/EmoticonPanel/Scroll View/Viewport/Content/Button_StandUp");
        Assert.IsNotNull(GUIElement);
    }

    /////CHECK IF BUTTONS TEXT IS CORRECT/////////////////////
    [UnityTest]
    public IEnumerator Test_CheckScrollViewWaveButtonText()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas/EmoticonPanel/Scroll View/Viewport/Content/Button_Wave/Text (TMP)");
        TMP_Text ButtonText = GUIElement.GetComponent<TMP_Text>();
        Assert.AreEqual("Wave", ButtonText.text);
    }

    [UnityTest]
    public IEnumerator Test_CheckScrollViewShakeHandsButtonText()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas/EmoticonPanel/Scroll View/Viewport/Content/Button_ShakeHands/Text (TMP)");
        TMP_Text ButtonText = GUIElement.GetComponent<TMP_Text>();
        Assert.AreEqual("Shake Hands", ButtonText.text);
    }

    [UnityTest]
    public IEnumerator Test_CheckScrollViewSitDownButtonText()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas/EmoticonPanel/Scroll View/Viewport/Content/Button_SitDown/Text (TMP)");
        TMP_Text ButtonText = GUIElement.GetComponent<TMP_Text>();
        Assert.AreEqual("Sit Down", ButtonText.text);
    }

    [UnityTest]
    public IEnumerator Test_CheckScrollViewStandUpButtonText()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas/EmoticonPanel/Scroll View/Viewport/Content/Button_StandUp/Text (TMP)");
        TMP_Text ButtonText = GUIElement.GetComponent<TMP_Text>();
        Assert.AreEqual("Stand Up", ButtonText.text);
    }

    //////////////////////////////////////////////////////////
    /////CHECK ANIMATIONS/////////////////////////////////////
    [UnityTest]
    public IEnumerator Test_PlayerWaves()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas");
        PlayerEmoticonHandler CharacterAnim = GUIElement.GetComponent<PlayerEmoticonHandler>();
        CharacterAnim.WaveOnClick();
        yield return new WaitForSeconds(2.5f);
        Assert.AreEqual(true, CharacterAnim.IsWaving());
    }

    [UnityTest]
    public IEnumerator Test_PlayerShakesHand()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas");
        PlayerEmoticonHandler CharacterAnim = GUIElement.GetComponent<PlayerEmoticonHandler>();
        CharacterAnim.ShakeHandOnClick();
        yield return new WaitForSeconds(1.5f);
        Assert.AreEqual(true, CharacterAnim.IsShakingHands());
    }

    [UnityTest]
    public IEnumerator Test_PlayerSits()
    {
        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas");
        PlayerEmoticonHandler CharacterAnim = GUIElement.GetComponent<PlayerEmoticonHandler>();
        CharacterAnim.SitDownOnClick();
        yield return new WaitForSeconds(3.0f);
        Assert.AreEqual(true, CharacterAnim.IsSitting());
    }

    [UnityTest]
    public IEnumerator Test_PlayerStands()
    {

        yield return null;
        GameObject GUIElement = GameObject.Find("Canvas");
        PlayerEmoticonHandler CharacterAnim = GUIElement.GetComponent<PlayerEmoticonHandler>();
        CharacterAnim.SitDownOnClick();
        yield return new WaitForSeconds(2.5f);
        CharacterAnim.StandUpOnClick();
        yield return new WaitForSeconds(2.5f);
        Assert.AreEqual(true, CharacterAnim.IsStanding());
    }


    /////CHECK ROOM COMPONENTS////////////////////////////////

    [UnityTest]
    public IEnumerator Test_CheckRoomFloors()
    {
        yield return null;
        int floors = 2;
        int floors_found = 0;
        for (int i = 1; i <= floors; i++)
        {
            GameObject GUIElement = GameObject.Find("Room/Floors/Floor-"+i);
            if (GUIElement is not null)
            {
                floors_found += 1;
            }
        }
        Assert.AreEqual(floors, floors_found);
    }

    
    [UnityTest]
    public IEnumerator Test_CheckRoomWalls()
    {
        yield return null;
        int walls = 2;
        int walls_found = 0;
        for (int i = 1; i <= walls; i++)
        {
            GameObject GUIElement = GameObject.Find("Room/Walls/Wall-" + i);
            if (GUIElement is not null)
            {
                walls_found += 1;
            }
        }
        Assert.AreEqual(walls, walls_found);
    }

    [UnityTest]
    public IEnumerator Test_CheckRoomChairs()
    {
        yield return null;
        int chairs = 8;
        int chairs_found = 0;
        for (int i = 1; i <= chairs; i++)
        {
            GameObject GUIElement = GameObject.Find("Room/Chairs/Chair-" + i);
            if (GUIElement is not null)
            {
                chairs_found += 1;
            }
        }
        Assert.AreEqual(chairs, chairs_found);
    }

    [UnityTest]
    public IEnumerator Test_CheckRoomTables()
    {
        yield return null;
        int tables = 3;
        int tables_found = 0;
        for (int i = 1; i <= tables; i++)
        {
            GameObject GUIElement = GameObject.Find("Room/Tables/Table-" + i);
            if (GUIElement is not null)
            {
                tables_found += 1;
            }
        }
        Assert.AreEqual(tables, tables_found);
    }
}