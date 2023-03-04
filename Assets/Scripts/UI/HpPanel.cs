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
        }

        private void Start()
        {
            Statistics.Instance.OnLifeLeft += HpChanged;
            HpChanged(Statistics.Instance.LifeNumber);
        }

        private void OnDestroy()
        {
            Statistics.Instance.OnLifeLeft -= HpChanged;
        }

        private void HpChanged(int hp)
        {
            DestroyCurrentCells();
            CreateNewCells(hp);
        }

        private void CreateNewCells(int hp)
        {
            for (int i = 0; i < hp; i++)
            {
                GameObject cell = Instantiate(_cellPrefab, _rectTransform);
                _cells.Add(cell);
            }
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