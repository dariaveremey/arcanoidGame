using UnityEngine;

class ChangeLifeNumbersPickUp : PickUpBase
{
    #region Variables

    [Header(nameof(ChangeLifeNumbersPickUp))]
    [SerializeField] private int _lifesNumber;

    #endregion


    #region Private methods

    protected override void ApplyEffect(Collision2D col)
    {
        Statistics.Instance.IncrementLife(_lifesNumber);
    }

    #endregion
}