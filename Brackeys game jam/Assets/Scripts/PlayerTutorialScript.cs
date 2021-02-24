using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTutorialScript : MonoBehaviour
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

    public GameObject jumpparticle;

    public int steps;

    public GameObject leftRight;
    public GameObject Up;
    public GameObject makeClone;
    public GameObject jumponClone;
    public GameObject DestroyClone;
    public GameObject Challenge;

    public GameObject Congratulation;

    public GameObject orb;

    public Text NumberofClones;

    bool increasestep;

    int Jumpcount = 0;
    int LeftMousecount = 0;
    int RightMousecount = 0;
    int JumponcloneCount = 0;

    public RaycastHit2D hit1;

    public GameObject[] spawnpoints = new GameObject[5];

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        steps = 1;
        increasestep = false;
    }

    void Update()
    {
        if (steps == 1)
        {
            leftRight.SetActive(true);
        }
        else if(steps == 2)
        {
            leftRight.SetActive(false);
            Up.SetActive(true);
        }
        else if(steps == 3)
        {
            Up.SetActive(false);
            makeClone.SetActive(true);
        }
        else if (steps == 4)
        {
            makeClone.SetActive(false);
            jumponClone.SetActive(true);
        }
        else if(steps == 5)
        {
            jumponClone.SetActive(false);
            DestroyClone.SetActive(true);
            NumberofClones.gameObject.SetActive(true);
        }
        else if (steps == 6)
        {
            DestroyClone.SetActive(false);
            Challenge.SetActive(true);
        }
        else if(steps == 7)
        {
            Congratulation.SetActive(true);
            Challenge.SetActive(false);
            Destroy(Congratulation, 5);
            steps += 1;
        }

        if (steps >= 2 && steps <= 8)
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded == true)
            {
                GenerateParticle(jumpparticle);
                rb.velocity = Vector2.up * jumpstrength;
                FindObjectOfType<AudioManager>().Play("Jump");
                Jumpcount += 1;
            }
        }

        if(steps >= 3 && steps <= 8 )
        {
            if (Input.GetKeyDown(KeyCode.X) && isGrounded == false)
            {
                Instantiate(playerClone, transform.position, Quaternion.identity);
                LeftMousecount += 1;
            }
        }

        if (steps == 4)
        {

            if (isGrounded == true)
            {
                hit1 = Physics2D.Raycast(GroundCheck.position, Vector2.down, 0.2f);
                Debug.Log(hit1.collider.tag);
                if (hit1 != null && hit1.collider != null && hit1.collider.tag == "Clone")
                {
                    JumponcloneCount += 1;
                }
            }
        }

        if (steps >= 5 && steps <= 8)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector3.forward);
                if (hit.collider == null || hit.collider.tag == "Player")
                    return;
                else
                {
                    Destroy(hit.collider.gameObject);
                    RightMousecount += 1;
                }
            }
        }

        if(steps >= 6 && steps <= 8)
        {
            if (GameObject.FindGameObjectWithTag("Orb") == null)
            {
                Transform orbSpawn = spawnpoints[Random.Range(0, spawnpoints.Length)].transform;
                Instantiate(orb, orbSpawn.position, Quaternion.identity);
            }
        }
            
        if(Jumpcount == 1)
        {
            increasestep = true;
            Jumpcount += 1;

        }
        else if (RightMousecount == 1)
        {
            increasestep = true;
            RightMousecount += 1;
        }
        else if (LeftMousecount == 1)
        {
            increasestep = true;
            LeftMousecount += 1;
        }
        else if (JumponcloneCount == 1)
        {
            increasestep = true;
            JumponcloneCount += 1;
        }

        if (increasestep == true)
        {
            IncreaseStep();
            Debug.Log(steps);
        }

        if(NumberofClones.gameObject.activeSelf == true)
        {
            NumberofClones.text = "Clones: " + GameObject.FindGameObjectsWithTag("Clone").Length.ToString() + "/8";
        }
    }

    private void FixedUpdate()
    {
        if (steps == 2 || steps == 3 || steps == 4 || steps == 5 || steps == 6)
            isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, WhatisGround);

        xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * playerspeed, rb.velocity.y);
        if((xInput > 0 || xInput < 0) && steps == 1 )
        {
            increasestep = true;
        }
    }

    public void GenerateParticle(GameObject part)
    {
        GameObject par = Instantiate(part, GroundCheck.position, Quaternion.identity);
        par.transform.parent = transform;
        Destroy(par, 1);
    }

    public void IncreaseStep()
    {
        steps += 1;
        increasestep = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Orb")
        {
            Destroy(collision.gameObject);
            increasestep = true;
        }
    }
}
