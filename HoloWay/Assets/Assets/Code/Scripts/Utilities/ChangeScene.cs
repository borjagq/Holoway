using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private enum Scenes {
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
            case Scenes.LoginMenu:
                SceneManager.LoadScene(0);
                break;
            case Scenes.MainMenu:
                SceneManager.LoadScene(1);
                break;
            case Scenes.SettingsMenu:
                SceneManager.LoadScene(2);
                break;
            case Scenes.AvatarModificationMenu:
                SceneManager.LoadScene(3);
                break;
            case Scenes.RoomCreationMenu:
                SceneManager.LoadScene(4);
                break;
            case Scenes.RoomSelectionMenu:
                SceneManager.LoadScene(5);
                break;
            case Scenes.SmallRoom:
                SceneManager.LoadScene(6);
                break;
            case Scenes.MediumRoom:
                SceneManager.LoadScene(7);
                break;
            case Scenes.LargeRoom:
                SceneManager.LoadScene(8);
                break;
            case Scenes.MultiplayerRoom:
                SceneManager.LoadScene(9);
                break;
        }
        
    }
}