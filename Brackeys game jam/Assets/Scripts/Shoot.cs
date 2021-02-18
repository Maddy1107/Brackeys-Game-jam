using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Transform firepoint;

    public float firerate = 1f;

    public float nextspawnTime;

    public GameObject bulletPrefab;

    public string direction;

    void Update()
    {
        if(nextspawnTime < Time.time)
        {
            ShootBullet();
            nextspawnTime = Time.time + firerate;
        }
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, Quaternion.identity);
        Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
        if(direction == "Right")
        {
            bulletrb.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
        }
        if(direction == "Left")
        {
            bulletrb.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
        }
    }
}
