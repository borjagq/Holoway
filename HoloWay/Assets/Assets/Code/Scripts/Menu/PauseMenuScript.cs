using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject SettingsUI;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            //GlobalGameSettings.Instance.
            GameState CurrentGameState = GlobalGameSettings.Instance.GameState.GetGameState();
            switch(CurrentGameState)
            {
                    case GameState.InGame:
                    {
                        SettingsUI.SetActive(true);
                        GlobalGameSettings.Instance.GameState.SetGameState(GameState.InMenu);
                        break;
                    }
                    case GameState.InMenu:
                    {
                        SettingsUI.SetActive(false);
                        GlobalGameSettings.Instance.GameState.SetGameState(GameState.InGame);
                        break;
                    }
            }
            GlobalGameSettings.Instance.ChangeVolumeAccordingToState();
        }
    }
}
