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

    protected override bool CanAttack()
    {
        if(player == null)
            return false;
        
        return base.CanAttack() & atOptimalDistance;
    }

    protected override void OnAttack()
    {
        if(projectilePrefab != null)
        {
            GameObject projectileObj = Instantiate(projectilePrefab, attackPoint.position, attackPoint.rotation);
            if(projectileObj.TryGetComponent(out Projectile projectile))
            {
                projectile.OverrideDamage(attackDamage);
                projectile.IgnoreObject(gameObject);
            }

        }
    }

    private bool atOptimalDistance;
    protected override Vector3 Navagate()
    {
        if(player == null)
            return transform.position;

        Vector3 dir = transform.position - player.position;
        //ignore vertical difference
        dir = new Vector3(dir.x, 0, dir.z);
        //consider planer distance from player

        float playerDistance = dir.magnitude;
        if(playerDistance < minimumPlayerDistance || playerDistance > maximumlayerDistance)
        {
            atOptimalDistance = false;
            //flee to a fixed position directly away from the player
            Vector3 fleeDirection = transform.position - player.position;
            fleeDirection = new Vector3(fleeDirection.x, transform.position.y, fleeDirection.z);
            fleeDirection.Normalize();
            Vector3 fleePosition = player.position + fleeDirection * optimalPlayerDistance;

            return fleePosition;
        }else
        {            
            atOptimalDistance = true;
            return transform.position;
        }
    }
}
