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

    public Transform playerClone;

    public List<Transform> Clones;

    float xInput;

    Spawner spawn1;

    public GameObject jumpparticle;

    public GameObject blastparticlelava;

    public GameObject blastparticlespike;

    bool isEnd = false;

    public float endTime = 3f;

    public GameObject end;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawn1 = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameplay == true)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            if (Input.GetButtonDown("Jump") && isGrounded == true)
            {
                GenerateParticle(jumpparticle);
                rb.velocity = Vector2.up * jumpstrength;
                FindObjectOfType<AudioManager>().Play("Jump");
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (Clones.Count != 8)
                {
                    Clones.Add(Instantiate(playerClone, transform.position, Quaternion.identity));
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector3.forward);
                if (hit.collider == null || hit.collider.tag == "Player")
                    return;
                else
                    Destroy(hit.collider.gameObject);
                Clones.Remove(hit.collider.transform);
            }
            if (isEnd == true)
            {
                if(endTime >= 0)
                {
                    Camera.main.transform.position = transform.position - new Vector3(0, 0, 10);
                    transform.Translate(new Vector2(0, Time.deltaTime));
                    endTime -= Time.deltaTime;
                }
                else
                {
                    if(transform.position.y - Camera.main.transform.position.y < Camera.main.orthographicSize - 5)
                    {
                        transform.Translate(new Vector2(0, 0.000001f * Time.deltaTime));
                        Vector2 playerSize = transform.localScale;
                        playerSize += new Vector2(3, 3f) * Time.deltaTime;
                        transform.localScale = playerSize;
                    }
                    else
                    {
                        rb.velocity = Vector2.zero;
                        rb.gravityScale = 0;
                        GameManager.instance.pauseGamePlay();
                    }
                    end.SetActive(true);
                }
                rb.gravityScale = 0;
                Camera.main.orthographicSize = 20;
            }
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameplay == true)
        {
            isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, WhatisGround);

            xInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(xInput * playerspeed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LevelTrigger1" || collision.tag == "LevelTrigger2" || collision.tag == "LevelTrigger3")
        {
            Debug.Log(GameManager.instance.levelnum);
            Camera.main.orthographicSize += 10;
            collision.gameObject.SetActive(false);
            GameManager.instance.setLevelNum();
            GameManager.instance.EnableLevel();
        }
        if (collision.tag == "LevelTrigger4")
        {
            GameManager.instance.
                DisableAllLevel();
            isEnd = true;
        }
        if (collision.tag == "Lava")
        {
            GenerateParticle(blastparticlelava);
            spawn1.Spawn();
        }
        if(collision.tag == "Spikes")
        {
            GenerateParticle(blastparticlespike);
            spawn1.Spawn();
        }
        if (collision.tag == "Bullet")
        {
            GenerateParticle(blastparticlespike);
            spawn1.Spawn();
        }
    }

    public void GenerateParticle(GameObject part)
    {
        GameObject par = Instantiate(part, GroundCheck.position, Quaternion.identity);
        par.transform.parent = transform;
        Destroy(par, 1);
    }
}
