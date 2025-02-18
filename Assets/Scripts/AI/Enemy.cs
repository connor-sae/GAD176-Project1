using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float targetDistanceThreshold;

    protected Vector3 targetPos;

    #region UnityFunctions

    protected virtual void Update()
    {
        if(Vector3.Distance(targetPos, transform.position) > targetDistanceThreshold)
        {
            GoToTarget();
        }
    }
    #endregion

    public abstract void Attack();


    private void GoToTarget()
    {
        Vector3 targetDir = targetPos - transform.position;
        targetDir.Normalize();
        transform.position += targetDir * moveSpeed * Time.deltaTime;
    }

}
