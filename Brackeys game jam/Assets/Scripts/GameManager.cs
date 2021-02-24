using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameplay;
    public bool gameOver;

    public int levelnum;

    public GameObject[] Levels = new GameObject[4];

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

    private void Start()
    {
        gameplay = false;
        levelnum = 0;
    }

    public void StartGame()
    {
        gameplay = true;
    }

    public void pauseGamePlay()
    {
        gameplay = false;
    }

    public void setLevelNum()
    {
        levelnum = levelnum + 1;
    }

    public void EnableLevel()
    {
        Levels[levelnum].SetActive(true);
    }

    public void DisableAllLevel()
    {
        foreach(GameObject l in Levels)
        {
            l.SetActive(false);
        }
    }
}
