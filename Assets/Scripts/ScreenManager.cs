using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private TextMeshProUGUI _scoreLable;
    [SerializeField] private TextMeshProUGUI _lifeLable;
    [SerializeField] private GameObject[] _heartPrefab;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        Statistics.Instance.OnScoreChanged += SetScoreLable;
        Statistics.Instance.OnLifeLeft += SetLifeLable;
        for (var i = 0; i < 5; i++)
        {
            int j = 100;
            j = +100;
            Instantiate(_heartPrefab[0], new Vector3(253 + j, -56, 0), Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        Statistics.Instance.OnScoreChanged -= SetScoreLable;
        Statistics.Instance.OnLifeLeft -= SetLifeLable;
    }

    #endregion


    #region Private methods

    private void SetScoreLable(int score)
    {
        _scoreLable.text = score.ToString();
    }

    private void SetLifeLable(int life)
    {
        _lifeLable.text = life.ToString();
    }

    #endregion
}