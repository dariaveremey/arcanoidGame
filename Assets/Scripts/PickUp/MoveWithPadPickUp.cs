using UnityEngine;

class MoveWithPadPickUp : PickUpBase
{
    #region MyRegion

    #region Private methods

    protected override void ApplyEffect(Collision2D col)
    {
        FindObjectOfType<Ball>().ToDefaultState();
    }

    #endregion

    #endregion
}