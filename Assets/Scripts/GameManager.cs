using UnityEngine;

public class GameManager : MonoBehaviour

{
    #region Variables

    [SerializeField] private Ball _ball;

    private bool _isStarted;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        FindObjectOfType<LevelManager>().OnBlocksDestroyed += PerformWin;
    }

    private void Update()
    {
        if (_isStarted)
            return;

        _ball.MoveWithPad();

        if (Input.GetMouseButtonDown(0))
        {
            StartBall();
        }
    }

    private void OnDestroy()
    {
        FindObjectOfType<LevelManager>().OnBlocksDestroyed -= PerformWin;
    }

    #endregion


    #region Private methods

    private void StartBall()
    {
        _isStarted = true;
        _ball.StartMove();
    }

    #endregion


    #region Public methods

    public void PerformWin()
    {
        SceneLoader.Instance.LoadScene();
        // TODO:REAL LOGIC
        //WinManager.Instance.ToggleWin();
    }

    #endregion
}