using UnityEngine;

public class LostZone : MonoBehaviour
{
    [Header("PadSize")]
    [SerializeField] private Ball _ball;

    private Vector3 _startBallposition;
    

    private void OnCollisionEnter2D(Collision2D col)

    {
        if (col.gameObject.CompareTag(Tags.Ball))
        {
            Statistics.Instance.IncrementLife(-1);
            Ball component = col.gameObject.GetComponent<Ball>();
            component.ToDefaultState();
            component.MoveWithPad();
        }
        else
        {
            Destroy(col.gameObject);
        }
    }
}