using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{

    [SerializeField] private Weapon weaponToPickup;
    [SerializeField] private GameObject placeHolder;

    void Start()
    {
        Instantiate(weaponToPickup.displayPrefab, transform);
        Destroy(placeHolder);
    }

    protected override void OnPickup(Player player)
    {
        player.weaponController.CollectWeapon(weaponToPickup);
    }


}
