using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponController))]
public class Player : Entity
{
    public WeaponController weaponController;
    protected override void Start()
    {
        base.Start();
        UIManager.UpdateHealth(health, maxHealth);
        weaponController = GetComponent<WeaponController>();
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        UIManager.UpdateHealth(health, maxHealth);
    }
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        UIManager.UpdateHealth(health, maxHealth);
    }
}
