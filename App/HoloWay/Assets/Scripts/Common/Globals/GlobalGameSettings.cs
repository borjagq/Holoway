using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameSettings 
{
    public static int CurrentSceneIndex = 0;
    public static int PreviousSceneIndex = 0;
    public static GlobalAudioSettings AudioSettings = new GlobalAudioSettings();

    public static int SCENE_INDEX_LOGIN = 0;
    public static int SCENE_INDEX_MAINMENU = 1;
    public static int SCENE_INDEX_ROOMCREATIONMENU = 2;
    public static int SCENE_INDEX_ROOMSELECTIONMENU = 3;
    public static int SCENE_INDEX_SETTINGSMENU = 4;
    public static int SCENE_INDEX_AVATARROOM = 5;
    public static int SCENE_INDEX_AVATARMODIFICATIONROOM = 6;
    public static int SCENE_INDEX_WHITEBOARDROOM = 7;
}
