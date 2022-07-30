using UnityEngine;

public class ScoreChangePickUp: PickUpBase
{
    [Header(nameof(ScoreChangePickUp))]
    [SerializeField] private int _score; 
    protected override void ApplyEffect(Collision2D col)
    {
        Statistics.Instance.IncrementScore(_score);
    }
}