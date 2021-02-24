using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingPlatform : MonoBehaviour
{
    Vector2 pos;
    Rigidbody2D platrb;
    float dashtime = 0.1f;
 
    void Start()
    {
        pos = transform.position;
        platrb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(dashtime >= 0)
        {
            dashtime -= Time.deltaTime;
            platrb.AddForce(Vector2.left * 50, ForceMode2D.Impulse);
        }
        else
        {
            platrb.velocity = Vector2.zero;
            transform.Translate(new Vector2(0, -20 * Time.deltaTime));
            if (transform.position.x >= pos.x)
            {
                dashtime = 0.1f;
            }
        }
    }
}
