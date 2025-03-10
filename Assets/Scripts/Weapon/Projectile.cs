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
    public List<GameObject> ignoreObjects;
    [SerializeField] float speed;
    [SerializeField] private int defaultDamage;
    [SerializeField] private float lifeTime = 1f;

    #region UnityFunctions

    void Awake()
    {
        GetComponent<Collider>().excludeLayers = ignoreLayers;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    #endregion

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    public void OverrideDamage(int value)
    {
        defaultDamage = value;
    }
    void OnCollisionEnter(Collision collision)
    {
        foreach(GameObject ignoreObject in ignoreObjects)
        {
            if(collision.collider.gameObject == ignoreObject)
                return;
        }

        if(collision.collider.TryGetComponent(out Entity entity))
            entity.TakeDamage(defaultDamage);
        
        Destroy(gameObject);
    }

    public void IgnoreObject(GameObject objectToIgnore)
    {
        ignoreObjects.Add(objectToIgnore);
    }

}
