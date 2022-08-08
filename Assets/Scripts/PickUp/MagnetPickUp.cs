using UnityEngine;

class MagnetPickUp : PickUpBase
{
    #region Variables

    [Header(nameof(MagnetPickUp))]
    [SerializeField] private float _time;

    #endregion


    #region Private methods

    protected override void ApplyEffect(Collision2D col)
    {
        Pad pad = FindObjectOfType<Pad>();
        pad.MagnetEffect(_time);
    }

    #endregion
}