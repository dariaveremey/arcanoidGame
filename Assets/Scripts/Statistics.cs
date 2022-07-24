using System;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : SingletonMonoBehavior<Statistics>
{
    #region Variables

    [SerializeField] private int _lifes;

    private int _lifeNumber = 5;

    #endregion


    #region Events

    public event Action<int> OnScoreChanged;
    public event Action<int> OnLifeLeft;
    public event Action OnLost;

    #endregion


    #region Properties

    public int ScoreNumber { get; private set; }

    #endregion


    #region Public methods

    public void IncrementScore(int score)
    {
        ScoreNumber += score;
        OnScoreChanged?.Invoke(ScoreNumber);
    }

    public void IncrementLife()
    {
        _lifeNumber -= 1;
        OnLifeLeft?.Invoke(_lifeNumber);
        HeartControler.Instance.HeartDestroy(_lifeNumber);
        if (_lifeNumber == 0)
        {
            PauseManager.Instance.TogglePause();
            OnLost?.Invoke();
        }
    }

    #endregion
}