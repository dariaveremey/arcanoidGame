using System;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : SingletonMonoBehavior<WinManager>
{
    #region Events

    public event Action<bool> OnGameWon;

    #endregion


    #region Properties

    public bool IsWon { get; private set; }

    #endregion


    #region Public methods

    public void Win()
    {
        IsWon = !IsWon;
        OnGameWon?.Invoke(IsWon);
    }

    #endregion
}