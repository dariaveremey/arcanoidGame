using System.Collections;
using UnityEngine;

public class Pad : MonoBehaviour

{
    #region Variables

    [Header("PadSize")]
    [SerializeField] private float _deltaTime=20f;
    private Ball _ball;
    private bool _isMagnetActive;
    private Vector2 _contactPoint;
    private readonly Vector3 _originalSize = Vector3.one;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        _ball = FindObjectOfType<Ball>();
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused)
        {
            return;
        }

        if (Statistics.Instance.NeedAutoPlay)
        {
            MoveWithMouBall();
        }
        else
        {
            MoveWithMouse();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_isMagnetActive && col.gameObject.CompareTag(Tags.Ball))
        {
            _contactPoint = (Vector2) transform.position - col.GetContact(0).point;
            Ball ball = col.gameObject.GetComponent<Ball>();
            ball.SetContactPoint(_contactPoint);
        }
    }

    #endregion


    #region Private methods

    private void MoveWithMouBall()
    {
        Vector3 ballPosition = _ball.transform.position;

        Vector3 currentPosition = transform.position;
        currentPosition.x = ballPosition.x;
        transform.position = currentPosition;
    }

    private void MoveWithMouse()
    {
        Vector3 mousePositionInPixels = Input.mousePosition;
        Vector3 mousePositionInUnits = Camera.main.ScreenToWorldPoint(mousePositionInPixels);

        Vector3 currentPosition = transform.position;
        currentPosition.x = mousePositionInUnits.x;
        transform.position = currentPosition;
    }

    private void ResetSize()
    {
        transform.localScale = _originalSize;
    }

    private IEnumerator WaitForEndMagnet(float time)
    {
        yield return new WaitForSeconds(time);
        _isMagnetActive = false;
        _ball.ResetOffset();
        
    }
    
    private IEnumerator WaitForEndChangeSize(float time)
    {
        yield return new WaitForSeconds(time);
        ResetSize();
    }

    #endregion


    #region Public methods

    public void ChangeSize(float scaleChange)
    {
        Vector3 changeScale = transform.localScale;
        changeScale = new Vector3(changeScale.x + scaleChange, changeScale.y,
            changeScale.z);
        StartCoroutine( WaitForEndChangeSize(_deltaTime));
    }

    public void MagnetEffect(float time)
    {
        _isMagnetActive = true;
        StartCoroutine(WaitForEndMagnet(time));
    }
    
    #endregion


    public void ResetPad()
    {
        transform.localScale = _originalSize;
    }
}