using System;
using UnityEngine;

public class Statistics : SingletonMonoBehavior<Statistics>
{
    #region Variables

    [SerializeField] private int _maxLifeNumber = 5;
    [SerializeField] private bool _needAutoPlay;
    #endregion


    #region MyRegion
    public bool NeedAutoPlay => _needAutoPlay;
    #endregion


    #region Events

    public event Action<int> OnScoreChanged;
    public event Action<int> OnLifeLeft;
    public event Action<bool> OnLost;

    #endregion
    


    #region Properties

    public int ScoreNumber { get; private set; }

    public int LifeNumber { get; private set; } = 5;

    #endregion


    #region Public methods

    public void IncrementScore(int score)
    {
        ScoreNumber += score;
        OnScoreChanged?.Invoke(ScoreNumber);
    }

    public void IncrementLife(int number)
    {
        LifeNumber += number;
        if (LifeNumber >= _maxLifeNumber)
            LifeNumber = _maxLifeNumber;
        OnLifeLeft?.Invoke(LifeNumber);

        if (LifeNumber != 0)
            return;
        PauseManager.Instance.TogglePause();
        OnLost?.Invoke(true);
        
    }

    public void ResetStatistics()
    {
        ScoreNumber = 0;
        LifeNumber = 0;
    }

    #endregion
}