using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLava : MonoBehaviour
{
    void Update()
    {
        Vector3 lavaScale = transform.localScale;
        lavaScale.y += 0.1f;
        transform.localScale = lavaScale;

        transform.Translate(new Vector2(0, 0.5f));
    }
}
