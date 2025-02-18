using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    private float attackDistanceThreashhold = 0.25f;
    [SerializeField] private float attackRadius = 0.8f;

    protected override void Update()
    {

        base.Update();
        if (player != null)
        {

            targetPos = player.position;

            if (Vector3.Distance(transform.position, player.position) <= attackDistanceThreashhold)
                Attack();
        }
    }

    public override void Attack()
    {
        if(CanAttack())
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius);

            foreach(Collider hit in hits)
            {
                Debug.Log(hit.name);
                if (hit.TryGetComponent(out Entity entity))
                {
                    Debug.Log("is entity");
                    if (hit.gameObject != this.gameObject)
                    {

                        Debug.Log(hit.name);
                        entity.TakeDamage(attackDamage);
                    }
                }
            }
        }


        base.Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
