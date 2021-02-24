using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    float xInput;

    Spawner spawn1;

    public GameObject jumpparticle;

    public GameObject blastparticlelava;

    public GameObject blastparticlespike;

    bool isEnd = false;

    public float endTime = 3f;

    public GameObject end;

    public Text NumberofClones;

    public Text Dest;

    public Text Checkpoint;

    float checkpointshowTime = 2;

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
            NumberofClones.gameObject.SetActive(true);
            Dest.gameObject.SetActive(true);
            rb.bodyType = RigidbodyType2D.Dynamic;
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded == true)
            {
                GenerateParticle(jumpparticle);
                rb.velocity = Vector2.up * jumpstrength;
                FindObjectOfType<AudioManager>().Play("Jump");
            }

            if (Input.GetKeyDown(KeyCode.X) && isGrounded == false)
            {
                if (GameManager.instance.Clones.Count != 8)
                {
                    GameManager.instance.Clones.Add(Instantiate(playerClone, transform.position, Quaternion.identity));
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector3.forward);
                if (hit.collider == null || hit.collider.tag == "Player")
                    return;
                else
                    Destroy(hit.collider.gameObject);
                GameManager.instance.Clones.Remove(hit.collider.transform);
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
                    if(transform.position.y - Camera.main.transform.position.y < Camera.main.orthographicSize - 10)
                    {
                        transform.Translate(new Vector2(0, 0.000001f * Time.deltaTime));
                        Vector2 playerSize = transform.localScale;
                        playerSize += new Vector2(2, 2f) * Time.deltaTime;
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

            if(Checkpoint.gameObject.activeSelf == true)
            {
                checkpointshowTime -= Time.deltaTime;
                if (checkpointshowTime <= 0)
                {
                    Checkpoint.gameObject.SetActive(false);
                    checkpointshowTime = 2;
                }
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
            spawn1.Spawn();
        }
        if(collision.tag == "Spikes")
        {
            spawn1.Spawn();
        }
        if (collision.tag == "Bullet")
        {
            spawn1.Spawn();
        }
        if(collision.tag == "CheckPointL3" || collision.tag == "CheckPointL4")
        {
            if (Spawner.instance.LevelSpawns[GameManager.instance.levelnum] != collision.gameObject)
            {
                Spawner.instance.LevelSpawns[GameManager.instance.levelnum] = collision.gameObject;
                Checkpoint.gameObject.SetActive(true);
            }
        }
    }

    public void GenerateParticle(GameObject part)
    {
        GameObject par = Instantiate(part, GroundCheck.position, Quaternion.identity);
        par.transform.parent = transform;
        Destroy(par, 1);
    }
}
