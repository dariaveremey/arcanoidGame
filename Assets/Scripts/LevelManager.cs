using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Viriables

    private int _blocksCount;

    #endregion


    #region Events

    public event Action OnBlocksDestroyed;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        Block.OnDestroyed += BlockDestroyed;
        Block.OnCreated += BlockCreated;
    }

    private void OnDestroy()
    {
        Block.OnDestroyed -= BlockDestroyed;
        Block.OnCreated -= BlockCreated;
    }

    #endregion


    #region Private methods

    private void BlockDestroyed(Block block)
    {
        _blocksCount--;
        if (_blocksCount == 0)

        {
            OnBlocksDestroyed?.Invoke();
            //SceneLoader.Instance.LoadScene();
        }
    }

    private void BlockCreated(Block block)
    {
        _blocksCount++;
    }

    #endregion
}