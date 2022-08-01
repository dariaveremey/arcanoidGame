using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Pad _pad;
    [SerializeField] private float _speed = 9f;
    [SerializeField] private float _minSpead = 1f;
    [SerializeField] private float _minSize = 0.5f;

    [Range(0, 1)]
    [SerializeField] private float _xMin;
    [Range(0, 1)]
    [SerializeField] private float _xMax;
    [Range(0, 1)]
    [SerializeField] private float _yMin;
    [Range(0, 1)]
    [SerializeField] private float _yMax;

    private bool _isStarted;
    private Vector2 _startDirection;
    private Vector2 _startMultiBall;
    private Vector3 _startPosition;
    private Vector3 _startScale;
    private Vector3 _currentBallPosition;

    [SerializeField] GameObject _ball;

    private readonly List<GameObject> _saveBalls = new List<GameObject>();

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        CreateBall();
        _startPosition = transform.position;
    }

    private void Start()
    {
        if (Statistics.Instance.NeedAutoPlay)
            StartBall();
    }

    private void Update()
    {
        _currentBallPosition = transform.position;
        if (_isStarted)
            return;

        MoveWithPad();
        
        MultiBall(false);

        if (Input.GetMouseButtonDown(0))
        {
            StartBall();
        }
    }

    #endregion


    #region Private methods

    private void CreateBall()
    {
        _saveBalls.Add(_ball);
    }

   /* private void CreateFalseBall()
    {
        _saveBalls.Add(Instantiate(_ball,
            new Vector3(transform.position.x, transform.position.y, transform.position.z)
            , Quaternion.identity, transform));
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _startDirection);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _rb.velocity);
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
        currentPosition.x = padPosition.x;
        transform.position = currentPosition;
    }

    public void ChangeSize(float scale)
    {
        Vector3 changeScale = transform.localScale;
        if (changeScale.x < _minSize || changeScale.y < _minSize)
        {
            changeScale =
                new Vector3(_minSize, _minSize, changeScale.z);
        }
        else
        {
            changeScale =
                new Vector3(changeScale.x + scale, changeScale.y + scale, changeScale.z);
        }
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

        if (velocityMagnitude < _minSpead) ;
        velocityMagnitude = _minSpead;

        _rb.velocity = velocity.normalized * velocityMagnitude;
    }

    public void MultiBall(bool activate)
    {
        if (activate == false || _saveBalls.Count==3)
            return;
        //foreach (var item in _saveBalls)
        for (int i = 0; _saveBalls.Count < 3; i++)
        { 
            _saveBalls.Add(Instantiate(_saveBalls[0], _saveBalls[0].transform.position, Quaternion.identity));

        }
    }

    #endregion
}