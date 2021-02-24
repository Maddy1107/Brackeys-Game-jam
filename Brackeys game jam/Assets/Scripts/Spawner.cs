using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject currplayer;

    public GameObject playerPrefab;

    public GameObject[] LevelSpawns = new GameObject[4];

    int levelnumber;

    private void Update()
    {
        currplayer = GameObject.FindGameObjectWithTag("Player");
    }

    public void Spawn()
    {
        levelnumber = GameManager.instance.levelnum;
        Destroy(currplayer);
        Instantiate(playerPrefab, LevelSpawns[levelnumber].transform.position, Quaternion.identity);
        Debug.Log(LevelSpawns[GameManager.instance.levelnum].gameObject.name);
    }
}
