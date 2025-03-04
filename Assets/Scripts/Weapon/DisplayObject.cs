using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof (Animator))]
public class DisplayObject : MonoBehaviour
{
    /// <summary>
    /// The point from which the bullet will be shot, tranform.forward is the direction
    /// </summary>
    public Transform shotPoint;

    private AnimatorController animator;

    void Start()
    {
        animator = GetComponent<AnimatorController>();
    }

    void Update()
    {
        
    }
}
