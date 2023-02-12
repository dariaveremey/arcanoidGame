using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class HpPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _cellPrefab;
        private RectTransform _rectTransform;
        private List<GameObject> _cells = new();

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            HpChanged(Statistics.Instance.LifeNumber);
        }

        private void Start()
        {
            Statistics.Instance.OnLifeLeft += HpChanged;
        }

        private void OnDestroy()
        {
            Statistics.Instance.OnLifeLeft -= HpChanged;
        }

        private void HpChanged(int hp)
        {
            DestroyCurrentCells();
            Instantiate(_cellPrefab, _rectTransform);
        }

        private void DestroyCurrentCells()
        {
            foreach (GameObject cell in _cells)
            {
                Destroy(cell);
            }
            _cells.Clear();
        }
    }
}