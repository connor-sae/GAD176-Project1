using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private Transform gunHolder;

    [SerializeField] private List<Weapon> weapons;
    private DisplayObject currentWeaponDisplay;
    private Weapon activeWeapon;
    private float lastShotTime;


    private Coroutine reloadRoutine;

    private void Start()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            //decouple inventory weapon from original scriptable object
            // allows multiple of the same type of weapon to have independant ammo
            weapons[i] = Instantiate(weapons[i]);

            //ensure all weapons are full
            weapons[i].ReloadInstant();
        }
        //try to equip the first weapon
        if (weapons[0] != null)
        {
            EquipWeapon(0);
            UIManager.UpdateAmmo(weapons[0].currentMagazineAmmo, weapons[1].currentMagazineAmmo);
        }
        
    }

    private void Update()
    {

        // use different input modes for Auto or semi-auto
        if(weapons.Count > 0 && activeWeapon != null)
            switch(activeWeapon.shootMode)
                {
                    case ShootMode.SEMIAUTO: if (Input.GetButtonDown("Fire1")) TryShoot();
                        break;
                    case ShootMode.AUTOMATIC: if (Input.GetButton("Fire1")) TryShoot(); 
                        break;
                }


        //check all possible keypad inputs alpha1 - alpha9 (49 - 57) therefor keypadi => i + 49
        //equip that weapon if it exists
        for(int i = 0; i < weapons.Count; i++)
        {
            if(i > 9) 
                break;
            if(Input.GetKeyDown((KeyCode)(i + 49)) && weapons[i] != null)
            {
                EquipWeapon(i);
            }
        }

        if(Input.GetKeyDown(KeyCode.R) && !activeWeapon.reloading)
        {
            if (activeWeapon != null)
            {
                //reload weapon
                currentWeaponDisplay.PlayReloadAnim();
                reloadRoutine = StartCoroutine(activeWeapon.ReloadRoutine());
            }
        }
    }

    private void TryShoot()
    {
        if (!CanShoot())
            return;

        Transform shotPoint = currentWeaponDisplay.shotPoint;
        StartCoroutine(activeWeapon.Shoot(shotPoint.position, shotPoint.rotation));

        lastShotTime = Time.time;
    }

    private bool CanShoot()
    {
        bool cooldownComplete = Time.time >= lastShotTime + activeWeapon.shotDelay;
        bool hasAmmo = activeWeapon.currentMagazineAmmo > 0;
        return cooldownComplete && hasAmmo;
    }

    //unequip current weapon equip new weapon
    private void EquipWeapon(int weaponIndexToEquip)
    {
        if(currentWeaponDisplay != null)
            Destroy(currentWeaponDisplay.gameObject);
        // could play anim

        if (reloadRoutine != null)
            StopCoroutine(reloadRoutine);
        activeWeapon.reloading = false;

        activeWeapon = weapons[weaponIndexToEquip];
        currentWeaponDisplay = Instantiate(activeWeapon.displayPrefab, gunHolder);
        UIManager.UpdateAmmo(activeWeapon.currentMagazineAmmo, weapons[weaponIndexToEquip].currentReserveAmmo);
        UIManager.UpdateGunName(activeWeapon.name);
    }

    /// <summary>
    /// Refills the currently active weapon if it is not full
    /// </summary>
    /// <returns>if the weapon was refilled</returns>
    public bool RefillCurrentWeaponAmmo()
    {
        if (!activeWeapon.isFull()) // more conditions can be added as needed ie. specific ammo types
        {
            activeWeapon.RefillAmmo();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Finds the Index of the given weapon in the inventory, returns -1 if the weapon cannot be found
    /// </summary>
    private int GetWeaponIndex(Weapon weaponToGet)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weaponToGet == weapons[i])
                return i;
        }
        Debug.LogWarning("Weapon Index not found, Returning -1");
        return -1;
    }

    //sets 0 to 1 and 1 to 0 (:P)
    private int Toggle01(int numIn)
    {
        return (int)((numIn - 0.5f) * -1 + 0.5f);
    }

}
