using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowEnemy : Enemy
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }


    protected override void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        Plane worldPlane = new Plane(Vector3.up, Vector3.zero);
        worldPlane.Raycast(mouseRay,out float intersectDistance);

        targetPos = mouseRay.GetPoint(intersectDistance);

        base.Update();
    }
}
