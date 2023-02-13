using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    #region Variables

    [Header("Lable")]
    [SerializeField] private TextMeshProUGUI _scoreLable;
    [SerializeField] private TextMeshProUGUI _lifeLable;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        Statistics.Instance.OnScoreChanged += SetScoreLable;
        Statistics.Instance.OnLifeLeft += SetLifeLable;
        SetScoreLable(Statistics.Instance.ScoreNumber);
        SetLifeLable(Statistics.Instance.LifeNumber);
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
        _scoreLable.text = $"Score: {score.ToString()}";
    }
    
    private void SetLifeLable(int life)
    {
        _lifeLable.text = $"Hp: {life.ToString()}";
    }

    #endregion
}