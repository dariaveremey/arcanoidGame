using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Pad _pad;
    [SerializeField] private Ball _ball;
    [SerializeField] private Rigidbody2D _rb;

    [Header("Balls characteristics")]
    [SerializeField] private float _speed = 9f;
    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _maxSpeed = 10f;

    [SerializeField] private float _minSize = 0.45f;
    [SerializeField] private float _maxSize = 1.95f;
    [SerializeField] private float _originalSpeed;
    [SerializeField] private float _offset = 6f;

    [Header("Random direction characteristics")]
    [Range(0, 1)]
    [SerializeField] private float _xMin;
    [Range(0, 1)]
    [SerializeField] private float _xMax;
    [Range(0, 1)]
    [SerializeField] private float _yMin;
    [Range(0, 1)]
    [SerializeField] private float _yMax;

    [Header("AudioSource")]
    [SerializeField] private AudioSource _audioSource;

    private bool _isStarted;
    private bool _isBallCloned;
    private bool _isNewBall;
    private bool _isExplosiveActive;
    private Vector2 _startDirection;
    private Vector2 _isMultiBall;
    private Vector3 _startPosition;
    private Vector3 _startScale;
    private Vector2 _contactPoint;

    private readonly Vector3 _originalSize = Vector3.one;
    private bool _isFireBallActive;

    #endregion


    public static event Action<Ball> OnBallFell;
    public static event Action<Ball> OnBallCreated;


    #region Unity lifecycle

    private void Start()
    {
        if (_isNewBall)
        {
            BallCreate(this);
            CancelMultiBall();
            return;
        }

        _speed = _originalSpeed;

        ResetBall();

        if (Statistics.Instance.NeedAutoPlay)
        {
            StartBall();
        }
    }

    private void Update()
    {
        if (_isStarted)
        {
            return;
        }

        MoveWithPad();

        if (Input.GetMouseButtonDown(0))
        {
            StartBall();
        }
    }

    public void Clone(Ball ball)
    {
        _isNewBall = true;
        _speed = ball._speed;
        StartBall();
    }

    public void CancelMultiBall()
    {
        _isNewBall = false;
    }

    public void OnBallFall()
    {
        OnBallFell?.Invoke(this);
        if (BallHandler.Instance.BallCount == 0)
        {
            ResetBall();
            _pad.ResetPad();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 transformPosition = transform.position;

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transformPosition, transformPosition + (Vector3) _startDirection);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transformPosition, transformPosition + (Vector3) _rb.velocity);
    }

    private void OnCollisionEnter2D()
    {
        _audioSource.Play();
    }

    #endregion


    #region Private methods

    private void BallCreate(Ball ball)
    {
        OnBallCreated?.Invoke(ball);
    }

    private void ResetBall()
    {
        BallCreate(_ball);
        _isStarted = false;
        _contactPoint = Vector2.zero;

        ResetSize();
        ResetSpeed();
        ResetOffset();
        MoveWithPad();
    }

    private void ResetSpeed()
    {
        _speed = _originalSpeed;
    }

    private void ResetSize()
    {
        transform.localScale = _originalSize;
    }

    public void ResetOffset()
    {
        _contactPoint = Vector2.zero;
        Vector3 hh = transform.position;
        hh.y -= _offset;
        transform.position = hh;
    }

    private void StartBall()
    {
        _isStarted = true;
        StartMove();
    }

    #endregion


    #region Public methods

    private void StartMove()
    {
        Vector2 randomDirection = new Vector2(Random.Range(_xMin, _xMax), Random.Range(_yMin, _yMax));
        _startDirection = randomDirection.normalized;
        _rb.velocity = _startDirection * _speed;
    }

    public void MoveWithPad()
    {
        Vector3 padPosition = _pad.transform.position;
        Vector3 currentPosition = transform.position;
        currentPosition.x = padPosition.x - _contactPoint.x;
        currentPosition.y = padPosition.y + _offset;
        transform.position = currentPosition;
    }

    public void ChangeSize(float multiplierScale)
    {
        Vector3 changeScale = transform.localScale;
        changeScale *= multiplierScale;
        if (changeScale.x < _minSize || changeScale.y < _minSize)
        {
            changeScale =
                new Vector3(_minSize, _minSize, changeScale.z);
        }

        if (changeScale.x > _maxSize || changeScale.y > _maxSize)
        {
            changeScale =
                new Vector3(_maxSize, _maxSize, changeScale.z);
        }

        transform.localScale = changeScale;
    }

    public void ToDefaultState()
    {
        _isStarted = false;
        _rb.velocity = Vector2.zero;
        transform.position = _startPosition;
        ResetSize();
    }

    public void ChangeSpeed(float speedMultiplier)
    {
        Vector2 velocity = _rb.velocity;
        float velocityMagnitude = velocity.magnitude;
        velocityMagnitude *= speedMultiplier;

        if (velocityMagnitude < _minSpeed)
            velocityMagnitude = _minSpeed;
        if (velocityMagnitude > _maxSpeed)
            velocityMagnitude = _maxSpeed;

        _rb.velocity = velocity.normalized * velocityMagnitude;
    }

    public void SetContactPoint(Vector2 contactPoint)
    {
        _isStarted = false;
        _contactPoint = contactPoint;
    }

    #endregion
}