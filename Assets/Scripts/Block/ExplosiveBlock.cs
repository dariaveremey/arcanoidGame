using System.Runtime.CompilerServices;
using UnityEngine;

public class ExplosiveBlock : Block
{
    [Header(nameof(ExplosiveBlock))]
    [SerializeField] private float _explosiveRadious;
    [SerializeField] private LayerMask _layerMask;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,_explosiveRadious);

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        DestroyActions();
    }

    public override void DestroyActions()
    {
        base.DestroyActions();
        Explode();
    }

    private void Explode()
    {
        Collider2D[] colliders=Physics2D.OverlapCircleAll(transform.position,_explosiveRadious,_layerMask);

        foreach (Collider2D collider1 in colliders)
        {
           Debug.Log($"Explode '{collider1.gameObject.name}'");
           Block blockToExplode = collider1.GetComponent<Block>();
           blockToExplode.DestroyActions();
        }
 
    }
}