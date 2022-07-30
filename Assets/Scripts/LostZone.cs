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

    private void OnCollisionEnter2D(Collision2D col)

    {
        if (col.gameObject.CompareTag(Tags.Ball))
        {
            Statistics.Instance.IncrementLife();
            Ball component = col.gameObject.GetComponent<Ball>();
            component.MoveWithPad(); 
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