using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMovingPlatform : MonoBehaviour
{
    public float movingspeedx;

    public float movingspeedy;

    public Transform sideLeft;

    public Transform sideRight;

    private void Update()
    {
        transform.Translate(new Vector2(movingspeedx,movingspeedy) * Time.deltaTime);

        RaycastHit2D hitLeft = Physics2D.Raycast(sideLeft.position,Vector2.left,0.2f);
        RaycastHit2D hitRight = Physics2D.Raycast(sideRight.position, Vector2.right, 0.2f);

        if(hitLeft.collider != null && hitRight.collider != null)
        {
            if(hitLeft.collider.name != "Player" && hitRight.collider.name != "Player")
            {
                movingspeedx = -movingspeedx;
            }
        }

    }
}
