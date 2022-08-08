using UnityEngine;

public class ExplosiveBlock : Block
{
    #region Variables

    [Header(nameof(ExplosiveBlock))]
    [SerializeField] private float _explosiveRadious;
    [SerializeField] private LayerMask _layerMask;

    #endregion


    #region Unity lifecycle

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosiveRadious);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        DestroyActions();
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosiveRadious, _layerMask);

        foreach (Collider2D collider1 in colliders)
        {
            Debug.Log($"Explode '{collider1.gameObject.name}'");
            Block blockToExplode = collider1.GetComponent<Block>();
            blockToExplode.DestroyActions();
        }
    }

    #endregion
}