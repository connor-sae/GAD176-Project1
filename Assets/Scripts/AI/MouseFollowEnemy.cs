using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowEnemy : Enemy
{
    protected override void OnAttack()
    {
        //throw new System.NotImplementedException();
    }

    protected override Vector3 Navagate()
    {
        Vector2 mousePos = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        Plane worldPlane = new Plane(Vector3.up, Vector3.zero);
        worldPlane.Raycast(mouseRay, out float intersectDistance);

        return mouseRay.GetPoint(intersectDistance);
    }

}
