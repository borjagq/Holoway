using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private enum Scenes {
        SplashScreen,
        LoginMenu,
        MainMenu,
        SettingsMenu,
        AvatarModificationMenu,
        RoomCreationMenu,
        RoomSelectionMenu,
        SmallRoom,
        MediumRoom,
        LargeRoom,
        MultiplayerRoom
    };

    [SerializeField]
    Scenes scenes = new Scenes();
    
    public void MoveToScene() {
        switch (scenes) {
            case Scenes.SplashScreen:
                SceneManager.LoadScene(0);
                break;
            case Scenes.LoginMenu:
                SceneManager.LoadScene(1);
                break;
            case Scenes.MainMenu:
                SceneManager.LoadScene(2);
                break;
            case Scenes.SettingsMenu:
                SceneManager.LoadScene(3);
                break;
            case Scenes.AvatarModificationMenu:
                SceneManager.LoadScene(4);
                break;
            case Scenes.RoomCreationMenu:
                SceneManager.LoadScene(5);
                break;
            case Scenes.RoomSelectionMenu:
                SceneManager.LoadScene(6);
                break;
            case Scenes.SmallRoom:
                SceneManager.LoadScene(7);
                break;
            case Scenes.MediumRoom:
                SceneManager.LoadScene(8);
                break;
            case Scenes.LargeRoom:
                SceneManager.LoadScene(9);
                break;
            case Scenes.MultiplayerRoom:
                SceneManager.LoadScene(10);
                break;
        }
        
    }
   
}