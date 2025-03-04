using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private Transform gunHolder;

    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private DisplayObject currentWeaponDisplay;
    private int activeWeapon = 0;
    private float lastShotTime;

    [Header("UI")]
    [SerializeField] private TMP_Text magazineAmmoUI;
    [SerializeField] private TMP_Text reserveAmmoUI;
    [SerializeField] private TMP_Text gunNameUI;

    private void Start()
    {
        if(weapons[0] != null)
            UnHolsterWeapon(0);
    }

    private void Update()
    {
        switch(weapons[activeWeapon].shootMode)
        {
            case ShootMode.SEMIAUTO: if (Input.GetButtonDown("Fire")) TryShoot();
                break;
            case ShootMode.AUTOMATIC: if (Input.GetButton("Fire")) TryShoot();
                break;
        }
    }

    private void TryShoot()
    {
        if (Time.time < lastShotTime + weapons[activeWeapon].shotDelay)
            return;

        Transform shotPoint = currentWeaponDisplay.shotPoint;
        StartCoroutine(weapons[activeWeapon].Shoot(shotPoint.position, shotPoint.rotation));

        lastShotTime = Time.time;
    }

    private void UnHolsterWeapon(int weaponToHolster)
    {
        Destroy(currentWeaponDisplay); // could play anim
        currentWeaponDisplay = Instantiate(weapons[weaponToHolster].displayPrefab, gunHolder.position, gunHolder.rotation);


        activeWeapon = weaponToHolster;
        //update UI;
    }

    private void UpdateUI()
    {
        Weapon c_weapon = weapons[activeWeapon];

        magazineAmmoUI.text = c_weapon.currentMagazineAmmo.ToString();
        reserveAmmoUI.text = c_weapon.currentReserveAmmo.ToString();
        gunNameUI.text = c_weapon.name.ToString();
    }

    //sets 0 to 1 and 1 to 0
    private int Toggle01(int numIn)
    {
        return (int)((numIn - 0.5f) * -1 + 0.5f);
    }

}
