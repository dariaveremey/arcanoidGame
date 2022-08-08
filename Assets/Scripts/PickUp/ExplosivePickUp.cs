using UnityEngine;

class ExplosivePickUp : PickUpBase
{
    [Header(nameof(ExplosivePickUp))]
    [SerializeField] private float _time;
    protected override void ApplyEffect(Collision2D col)
    {
        FindObjectOfType<Ball>().ExplosiveEffect(_time);
    }
}