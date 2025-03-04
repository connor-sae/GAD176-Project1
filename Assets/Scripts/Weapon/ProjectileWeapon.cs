using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class ProjectileWeapon : ScriptableObject
{


    [Header("Display Info")]
    [SerializeField] private new string name;
    [SerializeField] private DisplayObject displayPrefab;

    [Header("Basic")]
    [Tooltip("set to negative to use bullet default damage")]
    [SerializeField] protected int damage;
    [SerializeField] private float shotDelay;
    [SerializeField] private float spreadAngleSize;
    [SerializeField] private Vector2 recoil;
    public ShootMode shootMode;

    [Header("Burst Control")]
    [SerializeField] private int burstSize;
    [SerializeField] private float burstDelay;

    [Header("Ammunition")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int magazineSize;
    [SerializeField] private int maxAmmo;
    public int currentMagazineAmmo;
    public int currentReserveAmmo;

    public void RefillAmmo()
    {
        currentReserveAmmo = maxAmmo - currentMagazineAmmo;
    }

    public void Shoot(Vector3 shotOrigin, Quaternion shotAngle)
    {
        for(int i = 0; i < burstSize; i++)
        {
            // add random spread
            float randomX = Random.Range(-spreadAngleSize, spreadAngleSize);
            float randomY = Random.Range(-spreadAngleSize, spreadAngleSize);
            Vector3 angleDeviation = new Vector3(randomX, randomY, 0);
            Vector3 newDirection = shotAngle.eulerAngles + angleDeviation;

            Quaternion newAngle = Quaternion.Euler(newDirection);

            ShootSingle(shotOrigin, newAngle);
        }
        //reduce ammo?
        //recoil?
    }

    protected virtual void ShootSingle(Vector3 shotOrigin, Quaternion shotAngle)
    {
        Projectile projectile = Instantiate(projectilePrefab, shotOrigin, shotAngle).GetComponent<Projectile>();

        if (damage >= 0)
            projectile.OverrideDamage(damage);
    }
}

public enum ShootMode
{ 
    SEMIAUTO,
    AUTOMATIC,
}
