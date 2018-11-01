using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController: MonoBehaviour
{
    public Text timerText;

    public float totalTime;
    int seconds;

    public float destroyTime;

    public PlayerController PL1;
    public PlayerController PL2;
    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update ()
    {

        if (totalTime>0)
        {
            totalTime -= Time.deltaTime;
            seconds = (int)totalTime+1;
            
            if (totalTime <= 0)
            {
                //PL1.enabled = true;
                //PL2.enabled = true;
                timerText.text = "スタート";
            }
            else
            {
                //PL1.enabled = false;
                //PL2.enabled = false;
                timerText.text = seconds.ToString();
            }

        }

        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
        {
            Destroy(timerText);
        }
    }
}
