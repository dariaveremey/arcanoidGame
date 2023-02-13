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
    [SerializeField] protected SpriteRenderer _spriteRenderer;

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

    protected virtual void Start()
    {
        OnCreated?.Invoke(this);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ChangeDestroyNumber();
        SetSprite();
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

    protected virtual void SetSprite()
    {
        int index = _sprites.Length - _numberDestroy;
        if (_numberDestroy > 0)
        {
            _spriteRenderer.sprite = _sprites[index];
        }
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

    private void ChangeDestroyNumber()
    {
        _numberDestroy--;
        if (_numberDestroy == 0)
        {
            DestroyActions();
        }
    }

    #endregion
}