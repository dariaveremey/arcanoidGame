using UnityEngine;

public class LostZone : MonoBehaviour
{
    #region Viriables

    [Header("PadSize")]
    [SerializeField] private Ball _ball;
    //[SerializeField] private int _lifes;
    //[SerializeField] private int _coeficient;

    private Vector3 startBallposition;

    #endregion


    #region Unity lifycycle

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

    #endregion
}