using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text TimeElapsed;
    public Text TimeTaken;
    public float startTime;

    void Update()
    {
        if(GameManager.instance.gameplay == true)
        {
            TimeElapsed.gameObject.SetActive(true);
            float t = Time.time - startTime;

            string min = ((int)t / 60).ToString();
            string sec = ((int)t % 60).ToString();

            TimeElapsed.text = "Time:" + min + "m" + sec + "s";
        }
        else
        {
            string TimeElapsedText = TimeElapsed.text.Replace("Time:", "");
            TimeTaken.text = "Completed in:" + TimeElapsedText;
            TimeElapsed.text = TimeElapsed.text;
        }
    }

    public void setStartTime()
    {
        startTime = Time.time;
    }
}
