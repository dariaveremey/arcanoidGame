using UnityEngine;

public class GameManager : MonoBehaviour

{
    #region Variables

    //[SerializeField] private Ball _ball;

    private bool _isStarted;

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
        // TODO:REAL LOGIC
        //WinManager.Instance.ToggleWin();
    }

    #endregion
}