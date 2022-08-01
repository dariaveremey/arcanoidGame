using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonMonoBehavior<SceneLoader>
{
    #region Variables

    [SerializeField] private string _sceneName;

    #endregion


    #region Public methods

    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
    }

    #endregion
}