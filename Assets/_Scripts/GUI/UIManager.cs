using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager SI;

    private void Awake()
    {
        SI = SI == null ? this : SI;
    }

    #region inGame

    [SerializeField] private List<Image> _lifesImages;

    [SerializeField] private Text _countDownText;

    [SerializeField] private GameObject _countDownContainer;

    [SerializeField] private GameObject _gameOverContainer;

    [SerializeField] private Text _attemptsText;

    [SerializeField] private PlayableDirector _gameOverIN;

    [SerializeField] private PlayableDirector _win;

    [SerializeField] private PlayableDirector _attemptsIN;

    //Contenedor de la UI en el juego
    [SerializeField] private GameObject _hud;
    [SerializeField] private GameObject _gameOver;

    private List<Image> _initialLifesImages;

    #endregion

    #region inPause

    //Menus
    [SerializeField] private GameObject pauseMenu;

    #endregion

    void Start()
    {
        _initialLifesImages = new List<Image>(_lifesImages);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        HidePauseMenu();
    }

    public void ShowCountDown(int start)
    {
        _countDownContainer.SetActive(true);

        StartCoroutine(PlayCountDown(start));
    }

    private void HideCountDown()
    {
        _countDownContainer.SetActive(false);
    }

    private IEnumerator PlayCountDown(int start)
    {
        for (int i = start; i > 0; i--)
        {
            _countDownText.text = i.ToString();

            yield return new WaitForSeconds(1f);
        }

        HideCountDown();
        GameManager.SI.ChangeGameState(GameState.InGame);
        PhaseManager.SI.Pause(false);
    }

    public void LoseLife()
    {
        if (_lifesImages.Count > 0)
        {
            _lifesImages[_lifesImages.Count - 1].gameObject.SetActive(false);
            _lifesImages.Remove(_lifesImages[_lifesImages.Count - 1]);

        }
    }

    private void ActiveLifes()
    {
        for (int i = 0; i < _initialLifesImages.Count; i++)
        {
            _lifesImages.Add(_initialLifesImages[i]);
            _lifesImages[i].gameObject.SetActive(true);
        }
    }

    public void ResetGame(bool escene)
    {
        if (escene)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            ActiveLifes();
            _gameOver.SetActive(false);
            GameManager.SI.ChangeGameState(GameState.InGame);
            PlayerStats.SI.Respawn();
            PatternManager.SI.remainingPattern = PhaseManager.SI.GetCurrentPhase();
        }

    }

    public void RefreshAttempts(int number)
    {
        _attemptsText.text = number.ToString();
    }

    public void InitGame()
    {
        GameManager.SI.ChangeGameState(GameState.InGame);

    }

    public void InitGameDelayed(float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
        }

        GameManager.SI.ChangeGameState(GameState.InGame);
    }

    public void PlayTimeLineWin()
    {
        _win.Play();
    }

    public void PlayTimeLineGameOver()
    {
        _gameOverIN.Play();
    }

    public void PlayTimeLineAttemps(int number)
    {
        RefreshAttempts(number);
        if (_attemptsIN.state != PlayState.Playing) _attemptsIN.Play();
    }
}
