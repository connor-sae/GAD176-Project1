using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    [SerializeField] private float attackDistanceThreashhold = 0.5f;
    [SerializeField] private float attackRadius = 0.8f;

    
    protected override Vector3 Navagate()
    {
        if (playerPoint != null)
        {
            return playerPoint.position;
        }else
            return transform.position;
    }

    protected override bool CanAttack()
    {
        if(playerPoint == null)
            return false;
        //ignore vertical difference
        Vector3 dir = new Vector2(attackPoint.position.x, attackPoint.position.z) - new Vector2(playerPoint.position.x, playerPoint.position.z);
        //consider planer distance from player
        return base.CanAttack() & (dir.magnitude <= attackDistanceThreashhold);
    }

    protected override void OnAttack()
    {
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRadius);

        foreach(Collider hit in hits)
        {
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

    
}
