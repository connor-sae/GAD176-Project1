using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{

    [SerializeField] private bool singleUse = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            OnCollected(player);
            if(singleUse)
                Destroy(gameObject);
        }
    }

    protected abstract void OnCollected(Player player);
}
