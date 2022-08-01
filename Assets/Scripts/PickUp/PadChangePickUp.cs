using UnityEngine;

class PadChangePickUp : PickUpBase
{
    #region Variables

    [Header(nameof(PadChangePickUp))]
    [SerializeField] private float _scaleChange;

    #endregion


    #region Private methods

    protected override void ApplyEffect(Collision2D col)
    {
        FindObjectOfType<Pad>().ChangeSize(_scaleChange);
    }

    #endregion
}