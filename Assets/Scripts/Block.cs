using System;
using UnityEngine;
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
    [Range(0f, 1f)]
    [SerializeField] private float _pickUpSpawnChance;
    [SerializeField] private PickUpInfo[] _pickUpInfoArray;

    [SerializeField] private AudioClip _audioClip;

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
        SetSprite();
        Visibility();
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    private void OnValidate()
    {
        if (_pickUpInfoArray == null || _pickUpInfoArray.Length == 0)
        {
            return;
        }

        foreach (PickUpInfo pickUpInfo in _pickUpInfoArray)
        {
            pickUpInfo.OnVolidate();
        }
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
        if (_pickUpInfoArray == null && _pickUpInfoArray.Length == 0)
        {
            return;
        }

        float random = Random.Range(0, 1f);
        if (random > _pickUpSpawnChance)
        {
            return;
        }

        int chanceSum = 0;

        foreach (PickUpInfo pickUpInfo in _pickUpInfoArray)
        {
            chanceSum += pickUpInfo.SpawnChance;
        }

        int randomChance = Random.Range(0, chanceSum);
        int currentChance = 0;
        int currentIndex = 0;


        for (int i = 0; i < _pickUpInfoArray.Length; i++)
        {
            PickUpInfo pickUpInfo = _pickUpInfoArray[i];
            currentChance += pickUpInfo.SpawnChance;


            if (currentChance >= randomChance)
            {
                currentIndex = i;
                break;
            }
        }

        PickUpBase pickUpPrefab = _pickUpInfoArray[currentIndex].PickUpPrefab;
        Instantiate(pickUpPrefab, transform.position, Quaternion.identity);
    }

    #endregion


    #region Public methods

    public virtual void DestroyActions()
    {
        AudioPlayer.Instance.PlaySound(_audioClip);
        Statistics.Instance.IncrementScore(_points);
        Destroy(gameObject);
        SpawnPickUp();
    }

    public void ChangeDestroyNumber()
    {
        _numberDestroy--;
        if (_numberDestroy == 0)
        {
            DestroyActions();
        }
    }

    #endregion
}