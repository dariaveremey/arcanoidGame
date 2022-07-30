using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour //UIController
{
    #region Variables

    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _pauseMenuScreen;
    [SerializeField] private GameObject _gameWinScreen;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;

    //[SerializeField] private TextMeshProUGUI _gameOverLable;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        _gameOverScreen.SetActive(false);
        _pauseMenuScreen.SetActive(false);
        _gameWinScreen.SetActive(false);
    }

    private void Start()
    {
        Statistics.Instance.OnLost += GameOver;
        PauseManager.Instance.OnPaused += ContinueGame;
        _continueButton.onClick.AddListener(PauseManager.Instance.TogglePause);
        _exitButton.onClick.AddListener(ExitGame.ExitButtonClicked);
        //WinManager.Instance.OnGameWon  += GameWin;
    }

    private void OnDestroy()
    {
        Statistics.Instance.OnLost -= GameOver;
        PauseManager.Instance.OnPaused -= ContinueGame;
        //WinManager.Instance.OnGameWon  -= GameWin;
    }

    #endregion


    #region Private methods

    private void GameOver(bool isGameOver)
    {
        _gameOverScreen.SetActive(isGameOver);
    }

    private void ContinueGame(bool isPaused)
    {
        _pauseMenuScreen.SetActive(isPaused);
    }

    private void GameWin(bool isWon)
    {
        _gameWinScreen.SetActive(isWon);
    }

    #endregion
}