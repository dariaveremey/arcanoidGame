using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : SingletonMonoBehavior<PauseManager>
{
    #region Events

    public event Action<bool> OnPaused;

    #endregion


    #region Properties

    public bool IsPaused { get; private set; }

    #endregion


    #region Unity lyfecycle

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            OnPaused?.Invoke(IsPaused);
        }
    }

    #endregion


    #region Public methods

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0 : 1;
        OnPaused?.Invoke(IsPaused);
    }

    #endregion
}