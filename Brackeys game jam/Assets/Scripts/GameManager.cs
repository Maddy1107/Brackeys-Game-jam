using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameplay;
    public bool gameOver;

    public int levelnum;

    public GameObject[] Levels = new GameObject[4];

    public List<Transform> Clones;

    public Text NumberofClones;

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
        NumberofClones.text = "Clones: " + GameObject.FindGameObjectsWithTag("Clone").Length + "/8";
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
