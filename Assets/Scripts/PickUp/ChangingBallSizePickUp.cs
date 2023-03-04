using UnityEngine;

class ChangingBallSizePickUp : PickUpBase
{
    #region Variables

    [Header(nameof(ChangingBallSizePickUp))]
    [SerializeField] private float _scaleChange;

    #endregion


    #region Private methods

    protected override void ApplyEffect(Collision2D col)
    {
        foreach (Ball ball in BallHandler.Instance.Balls)
        {
            FindObjectOfType<Ball>().ChangeSize(_scaleChange);
        }
    }

    #endregion
}