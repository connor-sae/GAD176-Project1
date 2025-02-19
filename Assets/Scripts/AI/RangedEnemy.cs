using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] float minimumPlayerDistance = 2f;
    [SerializeField] float maximumlayerDistance = 5f;
    [SerializeField] float optimalPlayerDistance = 3.5f;
    [SerializeField] GameObject projectilePrefab;

    #region UnityFunctions

    protected override void Start()
    {
        base.Start();

        if(projectilePrefab == null)
            Debug.LogWarning(name + " has not been assigned a projectile prefab");

        //ensure optimal player distance is withing a viable range
        optimalPlayerDistance = Mathf.Clamp(optimalPlayerDistance, minimumPlayerDistance, maximumlayerDistance);
    }

    #endregion

    protected override void Attack()
    {
        if(projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, attackPoint.rotation);
            if(projectile.TryGetComponent(out ))
        }
    }

    protected override Vector3 Navagate()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);
        if(playerDistance < minimumPlayerDistance || playerDistance > maximumlayerDistance)
        {
            //flee to a fixed position directly away from the player
            Vector3 fleeDirection = transform.position - player.position;
            fleeDirection.Normalize();
            Vector3 fleePosition = player.position + fleeDirection * optimalPlayerDistance;

            return fleePosition;
        }else
        {
            TryAttack();
            
            return Vector3.zero;
        }
    }
}
