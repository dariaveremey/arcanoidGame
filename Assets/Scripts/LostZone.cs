using UnityEngine;
using UnityEngine.SceneManagement;

public class LostZone : MonoBehaviour
{
    #region Viriables

    [SerializeField] private Ball _ball;
    [SerializeField] private int _lifes;
    [SerializeField] private int _coeficient;

    private Vector3 startBallposition;

    #endregion


    #region Unity lifycycle

    private void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.gameObject.TryGetComponent(out Ball ball))
        {
            Statistics.Instance.IncrementLife();
            ball.MoveWithPad();
        }
    }

    #endregion


    #region Public methods

    public void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

    #endregion
}