using System;
using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    #region Viriables

    [Header("Block")]
    [SerializeField] private int _numberDestroy;
    [SerializeField] private int _points;

    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private bool _invisibility;

    [Header("PickUp")]
    [SerializeField] private GameObject[] _pickUpPrefab;

    [Range(0, 1f)]
    [SerializeField] private float _pickUpSpawnChange;

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
            SpawnPickUp();
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

    private void SpawnPickUp()
    {
        if (_pickUpPrefab.Length == default)
            return;

        float random = Random.Range(0, 1f);
        int randomIndex = Random.Range(0, _pickUpPrefab.Length);


        if (random <= _pickUpSpawnChange)
        {
            Instantiate(_pickUpPrefab[randomIndex], transform.position, Quaternion.identity);
        }
    }

    #endregion
}