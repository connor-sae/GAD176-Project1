using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float targetDistanceThreshold;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] protected int attackDamage = 1;
    [SerializeField] protected Transform attackPoint;

    private float lastAttackTime = 0;

    private Vector3 targetPos;
    protected Transform player { get;  private set; }

    #region UnityFunctions

    protected override void Start()
    {
        base.Start();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogWarning("No Player Exists in scene, Asign at least one object the 'Player' tag");
        }
        else
            player = playerObj.transform;
    }

    protected virtual void Update()
    {
        targetPos = Navagate();

        if(Vector3.Distance(targetPos, transform.position) > targetDistanceThreshold)
        {
            GoToTarget();
        }
    }
    #endregion

    /// <summary>
    /// Calls attack function if attack cooldown is complete
    /// </summary>
    public void TryAttack()
    {
        // enough time has not passed
        if (Time.time < lastAttackTime + attackCooldown)
            return;
        
        //tried to attack
        lastAttackTime = Time.time;
        
    }

    /// <summary>
    /// Performs Enemy Attack
    /// should not be performed directly, call TryAttack() first to consider attack cooldown
    /// </summary>
    protected abstract void Attack();

    /// <summary>
    /// called every frame
    /// </summary>
    /// <returns> the target position to navigate towards</returns>
    protected abstract Vector3 Navagate();


    private void GoToTarget()
    {
        Vector3 targetDir = targetPos - transform.position;
        targetDir.Normalize();
        transform.position += targetDir * moveSpeed * Time.deltaTime;
    }

}
