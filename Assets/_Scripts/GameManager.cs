﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    InGame,
    Pause,
    GameOver,
    Win
}

public class GameManager : MonoBehaviour
{
    public static GameManager SI;
    public GameState currentGameState = GameState.MainMenu;

    private void Awake()
    {
        SI = SI == null ? this : SI;
        ChangeGameState(GameState.MainMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeGameState(GameState.MainMenu);
            UIManager.SI.ShowPauseMenu();
        }
    }

    public void StartGame()
    {
        currentGameState = GameState.InGame;
    }

    public void ChangeGameState(GameState newGameState)
    {
        if (newGameState == GameState.InGame)
        {
            //PhaseConfigurator.SI.SetPhaseConfig();
        }

        if (newGameState == GameState.GameOver)
        {
            UIManager.SI.PlayTimeLineGameOver();
        }

        if (newGameState == GameState.Win)
        {
            UIManager.SI.PlayTimeLineWin();
        }

        currentGameState = newGameState;
    }

    public void GameReload()
    {
        //SceneManager.LoadScene(0);
    }
}