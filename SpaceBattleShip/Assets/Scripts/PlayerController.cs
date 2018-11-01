﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    /// <summary>
    /// 右にいるのか左にいるのか
    /// </summary>
    public enum SideState
    {
        left,
        Right
    }
    SideState m_sideState = SideState.left;

    [SerializeField]
    GameObject m_target;    //対戦相手

    float m_forward = 0f;   // 前進用変数
    float m_back = 0f;   // 後退用変数
    float m_left = 0f;   // 左移動用変数
    float m_right = 0f;   // 右移動用変数
    float m_up = 0f;       // 上移動
    float m_down = 0f;      //下移動

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move(); // 移動処理

        // 右にいるのか左にいるのかを判定
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        // 相手のスクリーン座標
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(m_target.transform.position);
        // X座標を比較して判定
        if (screenPos.x < targetScreenPos.x)
        {
            m_sideState = SideState.left;
        }
        else
        {
            m_sideState = SideState.Right;
        }
    }
    void Move()
    {

        // 前移動
        transform.position += transform.forward * m_forward * Time.deltaTime;

        // 後移動
        transform.position -= transform.forward * m_back * Time.deltaTime;
        // SPECIFICATION--
        // 右移動
        transform.position += transform.right * m_right * Time.deltaTime;

        // 左移動
        transform.position -= transform.right * m_left * Time.deltaTime;

        //上移動
        transform.position += Vector3.up * m_up * Time.deltaTime;

        //下移動
        transform.position -= Vector3.up * m_down * Time.deltaTime;

        // She-muC
        //	        Vector3 rot = transform.rotation.eulerAngles;
        //	        rot.y += m_right * Time.deltaTime;  // 右回転
        //	        rot.y -= m_left * Time.deltaTime;   // 左回転
        //	        transform.rotation = Quaternion.Euler(rot);
        //

        // 相手のほうを向く
        transform.LookAt(m_target.transform);

        // Y軸以外の回転をなくす
        Vector3 rot = transform.rotation.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);

        // 移動用変数を0に戻す
        m_forward = 0f;
        m_back = 0f;
        m_left = 0f;
        m_right = 0f;
        m_up = 0f;
        m_down = 0f;
    }
    /// <summary>
    /// 前移動命令.
    /// </summary>
    public float Forward
    {
        set
        {
            m_forward = value;
        }
    }


    /// <summary>
    /// 後移動命令.
    /// </summary>
    public float Back
    {
        set
        {
            m_back = value;
        }
    }


    /// <summary>
    /// 左移動命令.
    /// </summary>
    public float Left
    {
        set
        {
            m_left = value;
        }
    }


    /// <summary>
    /// 右移動命令.
    /// </summary>
    public float Right
    {
        set
        {
            m_right = value;
        }
    }

    //上移動命令
    public float Up
    {
        set
        {
            m_up = value;
        }
    }

    public float Down
    {
        set
        {
            m_down = value;
        }
    }

    /// <summary>
    /// 右にいるのか左にいるのか
    /// </summary>
    public SideState Side
    {
        get
        {
            return m_sideState;
        }
    }

}