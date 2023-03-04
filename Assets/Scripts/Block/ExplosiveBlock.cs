using UnityEngine;

public class ExplosiveBlock : Block
{
    #region Variables

    [Header(nameof(ExplosiveBlock))]
    [SerializeField] private float _explodeRadius;
    [SerializeField] private LayerMask _layerMask;

    #endregion


    #region Unity lifecycle

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _explodeRadius);
    }

    #endregion


    #region Public methods

    public override void DestroyActions()
    {
        base.DestroyActions();
        Explode();
    }

    #endregion


    #region Private methods

    private void Explode()
    {
        if (_explodeRadius != 0)
            return;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explodeRadius, _layerMask);

        foreach (Collider2D collider1 in colliders)
        {
            Block blockToExplode = collider1.GetComponent<Block>();
            blockToExplode.DestroyActions();
        }
    }

    #endregion
}