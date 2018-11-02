﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController: MonoBehaviour
{
    //表示されるテキスト
    public Text timerText;

    //カウントダウンの数値
    public float totalTime;
    int seconds;

    //スタート消すための時間、destroyTime-totalTime=表示時間
    public float destroyTime;

    //プレイヤー
    public PlayerController PL1;
    public PlayerController PL2;

    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update ()
    {
        //カウントダウン開始
        if (totalTime>0)
        {
            totalTime -= Time.deltaTime;
            seconds = (int)totalTime+1;

            //ifからは行動開始、elseはプレイヤーの行動は不可
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

        //文字の消去
        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
        {
            Destroy(timerText);
        }
    }
}
