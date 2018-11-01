﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserController : MonoBehaviour {

    PlayerController m_playerController;

    // 走っているかフラグ
    bool m_isRunning;

    // 上キーが離された時間を記録
    float m_timeOfKeyUp;

    [SerializeField]
    KeyCode m_forwardKey = KeyCode.RightArrow;      //前移動
    [SerializeField]
    KeyCode m_backKey = KeyCode.LeftArrow;          //後ろ移動
    [SerializeField]
    KeyCode m_rearKey = KeyCode.UpArrow;            //奥移動
    [SerializeField]
    KeyCode m_frontKey = KeyCode.DownArrow;         //手前移動
    [SerializeField]
    KeyCode m_upKey = KeyCode.RightShift;      　　 //上移動
    [SerializeField]
    KeyCode m_downKey = KeyCode.KeypadEnter;      　//下移動

    float m_forwardTimer;                           // 前キーを押した時刻を記録
    float m_backTimer;                              // 後ろキーを押した時刻を記録

    // Use this for initialization
    void Start () {
        m_playerController = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        // 画面の右左のどちらにいるかで方向キーを入れ替える
        KeyCode forwardKey = m_forwardKey;
        KeyCode backKey = m_backKey;

        if (m_playerController.Side == PlayerController.SideState.Right)
        {
            forwardKey = m_backKey;
            backKey = m_forwardKey;
        }

        // 前後移動
        if (Input.GetKey(forwardKey))
        {
            if (!m_isRunning)   // 走っていない時
            {
                if (Time.time - m_timeOfKeyUp < 0.3f)
                {
                    // 前回キーが押されてから0.3秒未満にもう一度押されたなら走る
                    m_isRunning = true;
                }
            }

            if (m_isRunning)
            {
                m_playerController.Forward = 5.0f;  // 走っているなら
            }
            else
            {
                m_playerController.Forward = 1.0f;  // 歩いているなら
            }

            //            m_playerController.Forward = 1.0f * 5f; // アナログ値 * 移動量(m/s)←最大値
        }
        else if (Input.GetKey(backKey))
        {
            m_playerController.Back = 1.0f;
            m_isRunning = false;
        }
        else
        {
            m_isRunning = false;
        }

        // 右キーが離された
        if (Input.GetKeyUp(forwardKey))
        {
            m_timeOfKeyUp = Time.time;
        }


        // 左右移動(もしくは回転)
        if (Input.GetKey(m_rearKey))
        {
            m_playerController.Left = 2.0f;
        }
        else if (Input.GetKey(m_frontKey))
        {
            m_playerController.Right = 2.0f;
        }

        if (Input.GetKey(m_upKey))
        {
            m_playerController.Up = 2.0f;
        }
        else if (Input.GetKey(m_downKey))
        {
            m_playerController.Down = 2.0f;
        }
    }
}