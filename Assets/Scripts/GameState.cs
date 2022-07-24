using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _pauseMenu;
    //[SerializeField] private GameObject _gameWin;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private TextMeshProUGUI _gameOverLable;

    #endregion


    #region Unity lifecycle

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

    private void GameOver()
    {
        _gameOver.SetActive(true);
    }

    private void ContinueGame(bool isPaused)
    {
        _pauseMenu.SetActive(isPaused);
    }

    private void GameWin(bool isWon)
    {
        //_gameWin.SetActive(isWon);
    }

    #endregion
}