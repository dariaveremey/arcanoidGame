using UnityEngine;

class MagnetPickUp : PickUpBase
{
    #region MyRegion

    [Header(nameof(MagnetPickUp))]
    [SerializeField] private float _time;
    
    protected override void ApplyEffect(Collision2D col)
    {
        Pad pad = FindObjectOfType<Pad>();
        pad.MagnetEffect(_time);
    }

    #endregion
}