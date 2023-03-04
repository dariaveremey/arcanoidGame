using UnityEngine;

class MultiBall : PickUpBase
{
    #region Variables

    [SerializeField] private int _ballCount = 2;

    #endregion


    #region Private methods

    protected override void ApplyEffect(Collision2D col)
    {

        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball ball in balls)
        {
            for (int i = 0; i < _ballCount; i++)
            {
                Ball newBall = Instantiate(ball, ball.transform.position, Quaternion.identity);
                newBall.Clone(ball);
            }
        }
  
    }

    #endregion
}