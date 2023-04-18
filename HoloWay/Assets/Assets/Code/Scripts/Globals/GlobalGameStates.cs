using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    InMenu,
    InGame
};

public class GlobalGameStates 
{
    private GameState gameState;
    public GlobalGameStates()
    {
        gameState = GameState.InMenu;
        
    }
    public GameState GetGameState()
    {
        return this.gameState;
    }
    public void SetGameState(GameState state)
    {
        this.gameState = state;
    }
}
