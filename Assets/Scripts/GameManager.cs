using UnityEngine;

public class GameManager : MonoBehaviour

{
    #region Variables

    private bool _isStarted;
    [SerializeField] private int _startHp;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        FindObjectOfType<LevelManager>().OnBlocksDestroyed += PerformWin;
    }

    private void OnDestroy()
    {
        FindObjectOfType<LevelManager>().OnBlocksDestroyed -= PerformWin;
    }

    #endregion


    #region Private methods

    #endregion


    #region Public methods

    public void PerformWin()
    {
        SceneLoader.Instance.LoadScene();
        WinManager.Instance.Win();
    }

    #endregion
}