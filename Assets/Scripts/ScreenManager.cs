using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private TextMeshProUGUI _scoreLable;
    [SerializeField] private TextMeshProUGUI _lifeLable;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        Statistics.Instance.OnScoreChanged += SetScoreLable;
        Statistics.Instance.OnLifeLeft += SetLifeLable;
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