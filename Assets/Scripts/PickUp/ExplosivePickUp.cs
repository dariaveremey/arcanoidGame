using UnityEngine;

class ExplosivePickUp : PickUpBase
{
    #region Variables

    [Header(nameof(ExplosivePickUp))]
    [SerializeField] private float _time;

    #endregion


    #region Private methods

    protected override void ApplyEffect(Collision2D col)
    {
        FindObjectOfType<Ball>().ExplosiveEffect(_time);
    }

    #endregion
}