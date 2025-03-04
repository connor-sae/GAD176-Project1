using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/RaycastWeapon", fileName = "new Raycast Weapon")]
public class RaycastWeapon : Weapon
{
    [Header("Raycast Settings")]
    /// <summary>
    /// use the camera to shoot the ray instead of the gun's shotpoint
    /// </summary>
    [SerializeField] private bool useCameraOrigin;
    [SerializeField] private float shotRange;


    /// <summary>
    /// Coroutine to shoot the next burst / shot using Raycasts
    /// </summary>
    /// <param name="shotOrigin"></param>
    /// where the ray will originate
    /// <param name="shotAngle"></param>
    /// the angle the ray will be cast in
    /// <returns></returns>
    protected override void ShootSingle(Vector3 shotOrigin, Quaternion shotAngle)
    {
        Ray shotRay;

        if (useCameraOrigin)
        {
            Transform camPoint = Camera.main.transform;
            shotRay = new Ray(camPoint.position, camPoint.rotation * Vector3.forward);
        } else
        {
            Vector3 shotDirection = shotAngle * Vector3.forward;
            shotRay = new Ray(shotOrigin, shotDirection);
        }

        if (Physics.Raycast(shotRay, out RaycastHit hit, shotRange))
        {
            if (hit.collider != null)
                if(hit.collider.TryGetComponent<Entity>(out Entity entity))
                {
                    entity.TakeDamage(damage);
                }
        }
    }
}
