using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (Collider))]
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
    protected Transform playerPoint { get;  private set; }
    protected Rigidbody m_rigidBody;

    #region UnityFunctions

    protected override void Start()
    {
        base.Start();

        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.useGravity = true;
        m_rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogWarning("No Player Exists in scene, Asign at least one object the 'Player' tag");
        }
        else
            playerPoint = playerObj.transform;
    }

    private void Update()
    {
        targetPos = Navagate();

        if(Vector3.Distance(targetPos, transform.position) > targetDistanceThreshold)
        {
            GoToTarget();
        }
        if(playerPoint != null)
            RotateTowards(playerPoint.position);

        TryAttack();
    }
    #endregion

    /// <summary>
    /// Calls OnAttack function if This enemy Can Attack is complete
    /// </summary>
    public void TryAttack()
    {
        // enough time has not passed
        if (!CanAttack())
            return;


        OnAttack();
        lastAttackTime = Time.time;

    }

    /// <summary>
    /// Performs Enemy Attack
    /// should not be performed directly, call TryAttack() first to consider attack conditions
    /// </summary>
    protected abstract void OnAttack();

    /// <summary>
    /// called every frame, handles navigation
    /// </summary>
    /// <returns> the target position to navigate towards</returns>
    protected abstract Vector3 Navagate();

    protected virtual bool CanAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }




    private void GoToTarget()
    {
        Vector3 targetDir = targetPos - transform.position;
        targetDir.Normalize();
        transform.position += targetDir * moveSpeed * Time.deltaTime;
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        float rot = Mathf.Atan2(dir.x, dir.z);
        //Debug.Log(rot * Mathf.Rad2Deg);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(rot * Mathf.Rad2Deg, Vector3.up), rotateSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, 1f);
    }

}
