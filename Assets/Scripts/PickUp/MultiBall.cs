using UnityEngine;

class MultiBall: PickUpBase
{
    #region Private methods

    [SerializeField] private int _multiBall;

        protected override void ApplyEffect(Collision2D col)
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball ball in balls)
        {
            for (int i = 0; i < _multiBall; i++)
            {
                    
            }
        }

    }

    #endregion
}