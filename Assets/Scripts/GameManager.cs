using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>

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
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.OnBlocksDestroyed -= PerformWin;
        }
    }

    #endregion


    #region Public methods

    public void PerformWin()
    {
        SceneLoader.Instance.LoadRandomScene();
    }

    public void ResetGame()
    {
        BallHandler.Instance.ResetBallHandler();
    }
    #endregion
}