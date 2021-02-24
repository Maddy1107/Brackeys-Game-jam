using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject currplayer;

    public GameObject playerPrefab;

    public GameObject[] LevelSpawns = new GameObject[4];

    int levelnumber;

    public static Spawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        currplayer = GameObject.FindGameObjectWithTag("Player");
    }

    public void Spawn()
    {
        levelnumber = GameManager.instance.levelnum;
        Destroy(currplayer);
        Instantiate(playerPrefab, LevelSpawns[levelnumber].transform.position, Quaternion.identity);
    }
}
