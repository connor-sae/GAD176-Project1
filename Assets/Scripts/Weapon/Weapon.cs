using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

[CreateAssetMenu(menuName ="Weapon/ProjectileWeapon", fileName = "new Projectile Weapon")]
public class Weapon : ScriptableObject
{


    [Header("Display Info")]
    public new string name;
    public DisplayObject displayPrefab;

    [Header("Basic")]
    [Tooltip("set to negative to use bullet default damage")]
    [SerializeField] protected int damage;
    [SerializeField] private float spreadAngleSize;
    [SerializeField] private Vector2 recoil;
    public float shotDelay;
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

    /// <summary>
    /// Coroutine to shoot the next burst / shot using projectiles
    /// </summary>
    /// <param name="shotOrigin"></param>
    /// where the projectile will originate
    /// <param name="shotAngle"></param>
    /// the angle the projectile will be shot at
    /// <returns></returns>
    public IEnumerator Shoot(Vector3 shotOrigin, Quaternion shotAngle)
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

            yield return new WaitForSeconds(burstDelay);
        }
        //reduce ammo?
        //recoil?
    }

    public void Reload()
    {
        int reloadAmount = Mathf.Min(magazineSize - currentMagazineAmmo, currentReserveAmmo);
        currentMagazineAmmo += reloadAmount;
        currentReserveAmmo -= reloadAmount;
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
