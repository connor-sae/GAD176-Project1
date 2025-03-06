using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public abstract class Weapon : ScriptableObject
{


    [Header("Display Info")]
    public new string name = "new Gun";
    public DisplayObject displayPrefab;

    [Header("Basic")]
    [Tooltip("set to negative to use bullet default damage")]
    [SerializeField] protected int damage = 1;
    [SerializeField] private float spreadAngleSize = 0;
    [SerializeField] private Vector2 recoil;
    public float shotDelay = 0.5f;
    public ShootMode shootMode;

    [Header("Burst Control")]
    [SerializeField] private int burstSize = 1;
    [SerializeField] private float burstDelay = 0;

    [Header("Ammunition")]
    [SerializeField] private int magazineSize = 10;
    [SerializeField] private int maxTotalAmmo = 50;
    [HideInInspector] public int currentMagazineAmmo;
    [HideInInspector] public int currentReserveAmmo;

    void OnEnable()
    {
        RefillAmmo();
        Reload();
    }
    
    public void RefillAmmo()
    {
        currentReserveAmmo = maxTotalAmmo - currentMagazineAmmo;
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
        UIManager.UpdateAmmo(currentMagazineAmmo, currentReserveAmmo);
    }



    protected abstract void ShootSingle(Vector3 shotPoint, Quaternion shotAngle);
}

public enum ShootMode
{ 
    SEMIAUTO,
    AUTOMATIC,
}
