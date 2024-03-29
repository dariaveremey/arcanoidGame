using UnityEngine;
using UnityEngine.UI;

public class UIscreenManager : MonoBehaviour
{
    #region Variables

    [Header("Screens")]
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _pauseMenuScreen;
    [SerializeField] private GameObject _gameWinScreen;

    [Header("Buttons")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;

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
        SceneLoader.Instance.OnGameWon += GameWin;
        _continueButton.onClick.AddListener(PauseManager.Instance.TogglePause);
        _exitButton.onClick.AddListener(ExitGame.ExitButtonClicked);
        _restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDestroy()
    {
        Statistics.Instance.OnLost -= GameOver;
        PauseManager.Instance.OnPaused -= ContinueGame;
        SceneLoader.Instance.OnGameWon -= GameWin;
    }

    #endregion


    #region Private methods

    private void GameOver(bool isGameOver)
    {
        _gameOverScreen.SetActive(isGameOver);
        GameManager.Instance.ResetGame();
        Statistics.Instance.ResetStatistics();
    }

    private void ContinueGame(bool isPaused)
    {
        _pauseMenuScreen.SetActive(isPaused);
    }

    private void GameWin(bool isWon)
    {
        _gameWinScreen.SetActive(isWon);
    }
    private void RestartGame()
    {
        GameWin(false);
        SceneLoader.Instance.LoadRandomScene();
        GameManager.Instance.ResetGame();
        Statistics.Instance.ResetStatistics();
    }
    #endregion
}