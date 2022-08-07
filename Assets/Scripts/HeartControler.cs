using UnityEngine;
using UnityEngine.UI;

public class HeartControler : SingletonMonoBehavior<HeartControler>
{
    #region Variables

    [SerializeField] private Image[] _heartSprite;
    [SerializeField] private GameObject[] _heartPrefab;

    #endregion

/*
    private void Start()
    {
        for (var i = 0; i < 5; i++)
        {
            int j = 100;
            j = +100;
            Instantiate(_heartPrefab[0], new Vector3(253 + j, -56, 0), Quaternion.identity);
        }

    }


    #region Private methods

    public void HeartDestroy(int index)
    {
        if (_heartSprite.Length != 0)
        {
            Destroy(_heartSprite[index]);
        }
    }




    #endregion*/
}