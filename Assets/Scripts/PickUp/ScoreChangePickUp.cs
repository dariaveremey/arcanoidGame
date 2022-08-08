using UnityEngine;

public class ScoreChangePickUp : PickUpBase
{
    #region Variables

    [Header(nameof(ScoreChangePickUp))]
    [SerializeField] private int _score;

    #endregion


    #region Private methods

    protected override void ApplyEffect(Collision2D col)
    {
        Statistics.Instance.IncrementScore(_score);
    }

    #endregion
}