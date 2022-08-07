using System.Collections;
using UnityEngine;

public class Pad : MonoBehaviour

{
    [SerializeField] private Vector3 _minSize;
    [SerializeField] private Vector3 _maxSize;

    private bool _isMagnetActive;
    private readonly Vector3 _originalSize = Vector3.one;
    private Vector2 _contactPoint;
    private Ball _ball;


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
    
    public void MagnetEffect(float time)
    {
        _isMagnetActive = true;
        StartCoroutine(WaitForEndMagnet(time));
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
    
    private void MoveWithMouse()
    {
        Vector3 mousePositionInPixels = Input.mousePosition;
        Vector3 mousePositionInUnits = Camera.main.ScreenToWorldPoint(mousePositionInPixels);

        Vector3 currentPosition = transform.position;
        currentPosition.x = mousePositionInUnits.x;
        transform.position = currentPosition;
    }
    
    private IEnumerator WaitForEndMagnet(float time)
    {
        yield return new WaitForSeconds(time);
        _isMagnetActive = false;
        _ball.ResetOffset();
        
    }

    private void ResetSize()
    {
        transform.localScale = _originalSize;
    }


    #endregion


    #region Public methods

    public void ChangeSize(float scaleChange)
    {
        Vector3 changeScale = transform.localScale;
        changeScale = new Vector3(changeScale.x + scaleChange, changeScale.y,
            changeScale.z);
    }
    
    public void ResetPad()
    {
        ResetSize();
    }

    #endregion
}