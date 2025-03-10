using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField] protected int maxHealth;

    protected int health;

    /// <summary>
    /// Decreases health by the given amount
    /// </summary>
    /// <param name="amount"></param>
    public virtual void TakeDamage(int amount)
    {
        health -= Mathf.Max(amount, 0); // damage cannot be negative

        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Increases health by the Given amount
    /// </summary>
    /// <param name="amount"></param>
    public virtual void Heal(int amount)
    {
        health += Mathf.Max(amount, 0); // damage cannot be negative

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }


    protected virtual void Start()
    {
        health = maxHealth;
    }


    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " was Killed");
        Destroy(gameObject);
    }
}
