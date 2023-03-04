using UnityEngine;

public class LostZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)

    {
        if (col.gameObject.CompareTag(Tags.Ball))
        {
            Ball ball = col.gameObject.GetComponent<Ball>();
            ball.ToDefaultState();
            ball.MoveWithPad();
            ball.OnBallFall();
        }
        else
        {
            Destroy(col.gameObject);
        }
    }
}