using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public int healAmount = 5;
    protected override void OnCollected(Player player)
    {
        player.Heal(healAmount);
    }
}
