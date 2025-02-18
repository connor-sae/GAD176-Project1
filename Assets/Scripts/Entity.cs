using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Collider))]
public class Entity : MonoBehaviour
{

    [SerializeField] private int maxHealth = 10;

    private int health;

    /// <summary>
    /// Decreases health by the given amount
    /// negative values are discarded
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        health -= Mathf.Max(amount, 0); //  cannot be negative

        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Increases health by the Given amount
    /// negative values are discarded
    /// </summary>
    /// <param name="amount"></param>
    public void Heal(int amount)
    {
        health += Mathf.Max(amount, 0); //  cannot be negative

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
