using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float targetDistanceThreshold;
    [SerializeField] private int maxHealth = 10;
    private int health;

    protected Vector3 targetPos;

    #region UnityFunctions

    protected virtual void Update()
    {
        if(Vector3.Distance(targetPos, transform.position) > targetDistanceThreshold)
        {
            GoToTarget();
        }
    }

    protected virtual void Start()
    {
        health = maxHealth;
    }
    #endregion

    public abstract void Attack();

    /// <summary>
    /// Decreases Enemy health by the given amount
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        health -= Mathf.Min(amount, 0); // damage cannot be posotive

        if(health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health += Mathf.Max(amount, 0); // damage cannot be negative

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    protected void Die()
    {
        Debug.Log(gameObject.name + " was Killed");
        Destroy(gameObject);
    }

    private void GoToTarget()
    {
        Vector3 targetDir = targetPos - transform.position;
        targetDir.Normalize();
        transform.position += targetDir * moveSpeed * Time.deltaTime;
    }

}
