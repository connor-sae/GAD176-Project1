using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    protected override void OnCollected(Player player)
    {
        player.weaponController.RefillCurrentWeaponAmmo();
    }
}
