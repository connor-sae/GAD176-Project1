using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : RangedEnemy
{
    [SerializeField] private float flyHeight = 4f;

    protected override void Start()
    {
        base.Start();
        m_rigidBody.useGravity = false;
    }

    protected override Vector3 Navagate()
    {
        Vector3 targetPos = base.Navagate();
        targetPos += Vector3.up * flyHeight;
        return targetPos;
    }
}
