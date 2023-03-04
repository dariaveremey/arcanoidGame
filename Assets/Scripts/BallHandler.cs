using System.Collections.Generic;

public class BallHandler : SingletonMonoBehavior<BallHandler>
{
    #region Properties

    public List<Ball> Balls { get; private set; } = new List<Ball>();
    public int BallCount => Balls.Count;

    #endregion


    #region Events

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        Ball.OnBallCreated += BallCreate;
        Ball.OnBallFell += BallDestroy;
    }

    private void OnDestroy()
    {
        Ball.OnBallCreated -= BallCreate;
        Ball.OnBallFell -= BallDestroy;
    }

    #endregion


    #region Public methods

    public void ResetBallHandler()
    {
        //Balls.Clear();
    }

    #endregion


    #region Private methods

    private void BallCreate(Ball ball)
    {
        Balls.Add(ball);
    }

    private void BallDestroy(Ball ball)
    {
        Balls.Remove(ball);
        if (BallCount == 0)
        {
            Statistics.Instance.IncrementLife(-1);
        }
    }

    #endregion
}