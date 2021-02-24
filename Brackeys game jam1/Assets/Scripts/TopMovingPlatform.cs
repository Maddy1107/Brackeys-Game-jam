using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopMovingPlatform : MonoBehaviour
{
    public float movingspeedx;

    public float movingspeedy;

    public Transform sideTop;

    public Transform sideBottom;

    private void Update()
    {
        transform.Translate(new Vector2(movingspeedx,movingspeedy) * Time.deltaTime);

        RaycastHit2D hitTop = Physics2D.Raycast(sideTop.position,Vector2.up,0.1f);
        RaycastHit2D hitBottom = Physics2D.Raycast(sideBottom.position, Vector2.down, 0.1f);

        if (hitTop.collider != null && hitBottom.collider != null)
        {
            if(hitTop.collider.name != "Player" && hitBottom.collider.name != "Player")
            {
                movingspeedy = -movingspeedy;
            }
        }

    }
}
