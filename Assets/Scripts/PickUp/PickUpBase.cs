using UnityEngine;

public abstract class PickUpBase : MonoBehaviour
{
    #region Variables

    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private int _addScore;

    #endregion


    #region Private methods

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag(Tags.Pad))
            return;
        ApplyEffect(col);
        AudioPlayer.Instance.PlaySound(_audioClip);
        Statistics.Instance.IncrementScore(_addScore);
        Destroy(gameObject);
    }
    

    protected abstract void ApplyEffect(Collision2D col);

    #endregion
}