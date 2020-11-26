using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public static int sceneReset = 0;

    private void Awake()
    {
        SI = SI == null ? this : SI;
        ChangeGameState(GameState.MainMenu);
    }

    private void Update()
    {
        if (currentGameState != GameState.InGame)
        {
            return;
        }
      
        sceneReset = PlayerPrefs.GetInt("TutorialRealizado");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeGameState(GameState.MainMenu);
            UIManager.SI.ShowPauseMenu();
        }
    }

    public void StartGame()
    {
        currentGameState = GameState.InGame;
        AudioManager.Instance.PlayNewTrack(1);
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
            UIManager.SI.ShowWin();
        }

        if(newGameState == GameState.MainMenu)
        {
            AudioManager.Instance.PlayNewTrack(0);
        }

        currentGameState = newGameState;
    }

    public void StartChronometer()
    {
        if(SceneManager.GetSceneByBuildIndex(1) == SceneManager.GetActiveScene())
        {
            UIManager.SI.ShowCountDown(0);
        }
    }

}
