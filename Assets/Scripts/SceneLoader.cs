using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneLoader : SingletonMonoBehavior<SceneLoader>
{
    #region Variables

    [Range(0, 4)]
    [SerializeField] private int _randomScene;

    [Header("AudioClip")]
    [SerializeField] private AudioClip _audioWonMusic;
    [SerializeField] private AudioClip _audioNewLevelMusic;

    private static List<string> _scenesNames =
        new List<string>() {"GameScene 1", "Level 2", "Level 3", "Level 4", "Level 5"};

    #endregion


    #region Events

    public event Action<bool> OnGameWon;

    #endregion

    //
    

    #region Public methods

    public void LoadRandomScene()
    {
        if (_scenesNames.Count == 0)
        {
            OnGameWon?.Invoke(true);
            AudioPlayer.Instance.PlaySound(_audioWonMusic);
            return;
        }

        _randomScene = Random.Range(0, _scenesNames.Count - 1);
        SceneManager.LoadScene(_scenesNames[_randomScene]);
        AudioPlayer.Instance.PlaySound(_audioNewLevelMusic);
        _scenesNames.Remove(_scenesNames[_randomScene]);
    }

    public void LoadStartSceneScene()
    {
        SceneManager.LoadScene(_scenesNames[_randomScene]);
        _scenesNames.Remove(_scenesNames[_randomScene]);
    }

    #endregion
}