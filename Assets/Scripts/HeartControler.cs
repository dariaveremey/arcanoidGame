using UnityEngine;
using UnityEngine.UI;

public class HeartControler : SingletonMonoBehavior<HeartControler>
{
    #region Variables

    [SerializeField] private Image[] _heartSprite;

    #endregion


    #region Private methods

    public void HeartDestroy(int index)
    {
        if (_heartSprite.Length != 0)
        {
            Destroy(_heartSprite[index]);
        }
    }

    #endregion
}