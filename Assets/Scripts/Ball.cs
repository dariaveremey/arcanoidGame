using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Pad _pad;
    [SerializeField] private float _speed = 9f;

    [Range(0, 1)]
    [SerializeField] private float _xMin;
    [Range(0, 1)]
    [SerializeField] private float _xMax;
    [Range(0, 1)]
    [SerializeField] private float _yMin;
    [Range(0, 1)]
    [SerializeField] private float _yMax;

    private Vector2 _startDirection;
    private Vector3 _startPosition;
    private bool _isStarted;

    #endregion


    private void Awake()
    {
        _startPosition = transform.position;
    }
    private void Update()
    {
        if (_isStarted)
            return;

        MoveWithPad();

        if (Input.GetMouseButtonDown(0))
        {
            StartBall();
        }
    }
    #region Private methods

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _startDirection);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _rb.velocity);
    }

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
    private void StartBall()
    {
        _isStarted = true;
        StartMove();
    }

    #endregion


    public void ToDefaultState()
    {
        _isStarted = false;
        _rb.velocity = Vector2.zero;
        transform.position = _startPosition;

    }
}