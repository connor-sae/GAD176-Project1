using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// ignore all objects in these layers
    /// </summary>
    [SerializeField] LayerMask ignoreLayers;
    /// <summary>
    /// ignore's all objects in list, less performant than ignoreLayers
    /// </summary>
    [SerializeField] GameObject[] ignoreObjects;
    [SerializeField] float speed;
    [SerializeField] private int damage;

    #region UnityFunctions

    void Awake()
    {
        GetComponent<Collider>().excludeLayers = ignoreLayers;
    }

    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    #endregion

    public void OverrideDamage(int value)
    {
        damage = value;
    }
    void OnCollisionEnter(Collision collision)
    {
        foreach(GameObject ignoreObject in ignoreObjects)
        {
            if(collision.collider.gameObject == ignoreObject)
            return;
        }

        if(collision.collider.TryGetComponent(out Entity entity))
            entity.TakeDamage(damage);

        Destroy(gameObject);
    }

}
