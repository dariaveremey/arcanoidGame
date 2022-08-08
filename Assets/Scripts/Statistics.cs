using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : SingletonMonoBehavior<Statistics>
{
    #region Variables

    [SerializeField] private int _lifes;
    [SerializeField] private bool _needAutoPlay;

    private int _lifeNumber = 5;

    #endregion


    #region MyRegion

    public bool NeedAutoPlay => _needAutoPlay;

    #endregion


    #region Events

    public event Action<int> OnScoreChanged;
    public event Action<int> OnLifeLeft;
    //public event Action<int> OnHeartLeft;
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
        if (LifeNumber >= 5)
            LifeNumber = 5;
        OnLifeLeft?.Invoke(LifeNumber);
        if (LifeNumber == 0)
        {
            PauseManager.Instance.TogglePause();
            OnLost?.Invoke(true);
        }
    }

    #endregion
}