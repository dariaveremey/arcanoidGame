using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Pad _pad;
    [SerializeField] private float _speed = 9f;
    [SerializeField] private float _minSpead = 1f;
    [SerializeField] private float _maxSpead = 10f;
    [SerializeField] private float _minSize = 0.45f;
    [SerializeField] private float _maxSize = 1.95f;
    
    [SerializeField] private float _explosiveRadious;
    [SerializeField] private LayerMask _layerMask;

    [Range(0, 1)]
    [SerializeField] private float _xMin;
    [Range(0, 1)]
    [SerializeField] private float _xMax;
    [Range(0, 1)]
    [SerializeField] private float _yMin;
    [Range(0, 1)]
    [SerializeField] private float _yMax;

    private bool _isStarted;
    private bool _isBallCloned;
    private bool _isExplosiveActive;
    private Vector2 _startDirection;
    private Vector2 _isMultiBall;
    private Vector3 _startPosition;
    private Vector3 _startScale;
    private Vector3 _currentBallPosition;
    private Vector2 _contactPoint;
   

    [SerializeField] private float _originalSpeed;
    [SerializeField] private float _originalOffset;
    [SerializeField] private float _offset;
    [SerializeField] private Ball _ball;

    [SerializeField] private AudioSource _audioSource;
    private readonly Vector3 _originalSize = Vector3.one;
    

    private readonly List<Ball> _saveBalls = new List<Ball>();

    public int SavedBallLength => _saveBalls.Count;
    private bool _isNewBall;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Start()
    {
        if (_isBallCloned)
        {
            BallCreate(this);
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
        _currentBallPosition = transform.position;
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

    private void OnCollisionEnter2D()
    {
        _audioSource.Play();
        if (_isExplosiveActive)
        {
            ExplosiveBlock();
        }

    }

    #endregion


    #region Private methods

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _startDirection);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _rb.velocity);
    }

    private void BallCreate(Ball ball)
    {
        _saveBalls.Add(ball);
    }

    private void BallDestroy(Ball ball)
    {
        _saveBalls.Remove(ball);
        if (SavedBallLength == 0)
        {
        }
    }
   

    private void ResetBall()
    {
        BallCreate(_ball);
        _isStarted = false;
        _contactPoint = Vector2.zero;

        ResetSize();
        ResetSpeed();
        MoveWithPad();
        ResetOffset();
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
        _offset = _originalOffset;
    }

    private void StartBall()
    {
        _isStarted = true;
        StartMove();
    }

    #endregion


    #region Public methods

    public void StartMove()

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

    public void ChangeSize(float scale)
    {
        Vector3 changeScale = transform.localScale;
        changeScale =
            new Vector3(changeScale.x * scale, changeScale.y * scale, changeScale.z);
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
    }

    public void ChangeSpead(float speedMultiplier)
    {
        Vector2 velocity = _rb.velocity;
        float velocityMagnitude = velocity.magnitude;
        velocityMagnitude *= speedMultiplier;

        if (velocityMagnitude < _minSpead)
        {
            velocityMagnitude = _minSpead;
        }
        
        _rb.velocity = velocity.normalized * velocityMagnitude;
    }

    public void SetContactPoint(Vector2 contactPoint)
    {
        _isStarted = false;
        _contactPoint = contactPoint;
        _offset = 6f;
    }

    public void OnBallFall()
    {
        BallDestroy(this);
        if (SavedBallLength == 0)
        {
        }
    }

    #endregion


    public void ExplosiveEffect(float time)
    {
        _isExplosiveActive = true;
        StartCoroutine(WaitForEndExplosive(time));
    }
    
    private IEnumerator WaitForEndExplosive(float time)
    {
        yield return new WaitForSeconds(time);
        _isExplosiveActive = false;

    }

    public void ExplosiveBlock()
    {
        Collider2D[] colliders=Physics2D.OverlapCircleAll(transform.position,_explosiveRadious,_layerMask);

        foreach (Collider2D collider1 in colliders)
        {
            Debug.Log($"Explode '{collider1.gameObject.name}'");
            Block blockToExplode = collider1.GetComponent<Block>();
            blockToExplode.DestroyActions();
        }
    }
    
}