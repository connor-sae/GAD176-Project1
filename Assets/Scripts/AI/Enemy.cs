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
    private float lastAttackTime = 0;

    protected Vector3 targetPos;
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

        if(Vector3.Distance(targetPos, transform.position) > targetDistanceThreshold)
        {
            GoToTarget();
        }
    }
    #endregion

    public virtual void Attack()
    {
        //able to attack
        if (CanAttack())
        {
            //tried to attack
            Debug.Log("Damn");
            lastAttackTime = Time.time;
        }
    }

    protected bool CanAttack()
    {
        return (Time.time >= lastAttackTime + attackCooldown);
    }



    private void GoToTarget()
    {
        Vector3 targetDir = targetPos - transform.position;
        targetDir.Normalize();
        transform.position += targetDir * moveSpeed * Time.deltaTime;
    }

}
