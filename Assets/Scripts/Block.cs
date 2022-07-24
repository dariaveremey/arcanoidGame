using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region Viriables

    [SerializeField] private int _numberDestroy;
    [SerializeField] private int _points;

    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private bool _invisibility;

    #endregion


    #region Events

    public static event Action<Block> OnCreated;
    public static event Action<Block> OnDestroyed;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        OnCreated?.Invoke(this);
        Invisibility();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ChangeDestroyNumber();
        ChangeScore();
        SetSprite();
        Visibility();
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    #endregion


    #region Private methods

    private void SetSprite()
    {
        int index = _sprites.Length - _numberDestroy;
        if (_numberDestroy > 0)
        {
            _spriteRenderer.sprite = _sprites[index];
        }
    }

    private void ChangeDestroyNumber()
    {
        _numberDestroy--;
    }

    private void ChangeScore()
    {
        if (_numberDestroy == 0)
        {
            Statistics.Instance.IncrementScore(_points);
            Destroy(gameObject);
            return;
        }
    }

    private void Invisibility()
    {
        if (_invisibility)
        {
            _spriteRenderer.color = new Color(255, 255, 255, 0);
        }
    }

    private void Visibility()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 255);
    }

    #endregion
}