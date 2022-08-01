using UnityEngine;

class MultiBall: PickUpBase
{
    #region Private methods

    protected override void ApplyEffect(Collision2D col)
    {
        //   foreach (Ball ball in FindObjectOfType<Ball>().Balls.ToList())
        //{
        //  FindObjectOfType<Ball>().MultiBall(true);
        //}
        FindObjectOfType<Ball>().MultiBall(true);
    }

    #endregion
}