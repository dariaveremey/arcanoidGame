using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneLoader : SingletonMonoBehavior<SceneLoader>
{
    #region Variables
    [Range(0,4)]
    [SerializeField] private int _randomScene;
    
    public event Action<bool> OnGameWon;
    
    public static List<string> _scenesNames = 
        new List<string>() { "GameScene 1", "Level 2", "Level 3", "Level 4", "Level 5" };
    
    #endregion

    


    #region Public methods

    public void LoadRandomScene()
    {
        if (_scenesNames.Count == 0)
        {
            OnGameWon?.Invoke(true);
            return;
        }
        _randomScene = Random.Range(0, _scenesNames.Count-1);
        SceneManager.LoadScene(_scenesNames[_randomScene]);
        _scenesNames.Remove(_scenesNames[_randomScene]);
        Debug.Log($"{_scenesNames.Count}");
   
    }

    public void LoadStartSceneScene()
    {
        SceneManager.LoadScene("Level 4");
        _scenesNames.Remove(_scenesNames[3]);
    }
    #endregion
}