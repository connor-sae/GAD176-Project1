using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    private float attackDistanceThreashhold = 0.25f;
    [SerializeField] private float attackRadius = 0.8f;


    protected override Vector3 Navagate()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) <= attackDistanceThreashhold)
                TryAttack();
            
            return player.position;
        }else
            return Vector3.zero;
    }

    protected override void Attack()
    {
        
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRadius);

        foreach(Collider hit in hits)
        {
            Debug.Log(hit.name);
            if (hit.TryGetComponent(out Entity entity))
            {
                if (hit.gameObject != this.gameObject)
                {
                    Debug.Log(hit.name);
                    entity.TakeDamage(attackDamage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
