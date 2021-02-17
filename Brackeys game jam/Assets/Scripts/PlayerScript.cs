using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float playerspeed = 10f;
    public float jumpstrength = 20f;

    public LayerMask WhatisGround;
    public bool isGrounded;
    public float checkRadius;
    public Transform GroundCheck;
    public Transform TopCheck;

    public Transform player;

    public List<Transform> Clones;

    float xInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpstrength;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Clones.Add(Instantiate(player, transform.position, Quaternion.identity));
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector3.forward);
            if (hit.collider == null)
                return;
            else
                Destroy(hit.collider.gameObject);
            Clones.Remove(hit.collider.transform);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, WhatisGround);

        xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * playerspeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LevelTrigger")
        {
            Camera.main.orthographicSize += 10;
            collision.gameObject.SetActive(false);
            
        }
    }
}
