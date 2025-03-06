using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Projectile Weapon", fileName = "new Projectile Weapon")]
public class ProjectileWeapon : Weapon
{
    
    [SerializeField] private GameObject projectilePrefab;


    /// <summary>
    /// Coroutine to shoot the next burst / shot using Raycasts
    /// </summary>
    /// <param name="shotOrigin"></param>
    /// where the ray will originate
    /// <param name="shotAngle"></param>
    /// the angle the ray will be cast in
    /// <returns></returns>
    protected override void ShootSingle(Vector3 shotOrigin, Quaternion shotAngle)
    {
        Projectile projectile = Instantiate(projectilePrefab, shotOrigin, shotAngle).GetComponent<Projectile>();

        if (damage >= 0)
            projectile.OverrideDamage(damage);
    }
}
