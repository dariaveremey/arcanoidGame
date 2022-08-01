using UnityEngine;

public class Pad : MonoBehaviour
{
    private Ball _ball;
    private bool _isCollisionPickUp;


    #region Unity lifecycle

    private void Start()
    {
        _ball = FindObjectOfType<Ball>();
        _isCollisionPickUp = false;
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused == true)
            return;
        if (Statistics.Instance.NeedAutoPlay)
        {
            MoveWithMouBall();
        }
        else
        {
            MoveWithMouse();
        }
    }

    private void MoveWithMouBall()
    {
        Vector3 ballPosition = _ball.transform.position;

        Vector3 currentPosition = transform.position;
        currentPosition.x = ballPosition.x;
        transform.position = currentPosition;
    }

    private void OnDestroy()
    {
    }

    private void MoveWithMouse()
    {
        Vector3 mousePositionInPixels = Input.mousePosition;
        Vector3 mousePositionInUnits = Camera.main.ScreenToWorldPoint(mousePositionInPixels);

        Vector3 currentPosition = transform.position;
        currentPosition.x = mousePositionInUnits.x;
        transform.position = currentPosition;
    }

    #endregion


    #region Public methods

    public void ChangeSize(float scaleChange)
    {
        Vector3 changeScale = transform.localScale;
        changeScale = new Vector3(changeScale.x + scaleChange, changeScale.y,
            changeScale.z);
    }

    #endregion
}