using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private Transform gunHolder;

    [SerializeField] private List<Weapon> weapons;
    private DisplayObject currentWeaponDisplay;
    private int activeWeaponIndex = 0;
    private float lastShotTime;

    private void Start()
    {
        if(weapons[0] != null)
            EquipWeapon(0);
    }

    private void Update()
    {
        if(weapons.Count > 0 && weapons[activeWeaponIndex] != null)
            switch(weapons[activeWeaponIndex].shootMode)
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
    }

    private void TryShoot()
    {
        if (Time.time < lastShotTime + weapons[activeWeaponIndex].shotDelay)
            return;

        Debug.Log("Made it");
        Transform shotPoint = currentWeaponDisplay.shotPoint;
        StartCoroutine(weapons[activeWeaponIndex].Shoot(shotPoint.position, shotPoint.rotation));

        lastShotTime = Time.time;
    }

    //unequip current weapon equip new weapon
    private void EquipWeapon(int weaponIndexToEquip)
    {
        Destroy(currentWeaponDisplay); 
        // could play anim

        activeWeaponIndex = weaponIndexToEquip;
        currentWeaponDisplay = Instantiate(weapons[activeWeaponIndex].displayPrefab, gunHolder);

        UIManager.UpdateAmmo(weapons[activeWeaponIndex].currentMagazineAmmo, weapons[weaponIndexToEquip].currentReserveAmmo);
        UIManager.UpdateGunName(weapons[activeWeaponIndex].name);
    }

    //sets 0 to 1 and 1 to 0
    private int Toggle01(int numIn)
    {
        return (int)((numIn - 0.5f) * -1 + 0.5f);
    }

}
